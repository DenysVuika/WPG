using System;
using System.Collections.Generic;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class GridEntryCollectionTest
  {
    #region Mock Objects

    private class CategoryItemMock : CategoryItem, IComparable<CategoryItemMock>
    {
      public CategoryItemMock(string name) : base(new PropertyGrid(), name) { }

      #region IComparable<CategoryItemMock> Members

      public int CompareTo(CategoryItemMock other)
      {
        return string.Compare(this.Name, other.Name);
      }

      #endregion
    }

    private class CategoryItemMockComparer : IComparer<CategoryItemMock>
    {
      public int Compare(CategoryItemMock x, CategoryItemMock y)
      {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
      }
    }

    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailInitializingNullValue()
    {
      new GridEntryCollection<CategoryItemMock>(null);
    }

    [TestMethod]
    public void ShouldInitializeAgainstExistingCollection()
    {
      CategoryItemMock category1 = new CategoryItemMock("one");
      CategoryItemMock category2 = new CategoryItemMock("two");
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>(new CategoryItemMock[] { category1, category2 });

      Assert.AreEqual<int>(2, collection.Count);
      Assert.AreEqual(category1, collection[0]);
      Assert.AreEqual(category2, collection[1]);
    }

    [TestMethod]
    public void ShouldReturnItemByName()
    {
      CategoryItemMock category1 = new CategoryItemMock("one");
      CategoryItemMock category2 = new CategoryItemMock("two");

      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      collection.Add(category1);
      collection.Add(category2);

      Assert.AreEqual(category1, collection["one"]);
      Assert.AreEqual(category2, collection["two"]);
    }

    [TestMethod]
    public void ShouldReturnNullForMissingName()
    {
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      Assert.IsNull(collection["missing"]);
      Assert.IsNull(collection[null]);
    }

    [TestMethod]
    public void ShouldRemoveItemByIndex()
    {
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      collection.Add(new CategoryItemMock("name1"));
      collection.Add(new CategoryItemMock("name2"));

      Assert.AreEqual<int>(2, collection.Count);

      collection.RemoveAt(0);

      Assert.AreEqual<int>(1, collection.Count);
      Assert.IsNull(collection["name1"]);
    }

    [TestMethod]
    public void ShouldRemoveAllItems()
    {
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      collection.Add(new CategoryItemMock("name1"));
      collection.Add(new CategoryItemMock("name2"));

      Assert.AreEqual<int>(2, collection.Count);

      collection.Clear();

      Assert.AreEqual<int>(0, collection.Count);
      Assert.IsNull(collection["name1"]);
      Assert.IsNull(collection["name2"]);
    }

    [TestMethod]
    public void ShouldRemplaceExistingItemByIndex()
    {
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      collection.Add(new CategoryItemMock("name1"));
      Assert.AreEqual<int>(1, collection.Count);

      CategoryItemMock item = new CategoryItemMock("name2");
      collection[0] = item;

      Assert.AreEqual<int>(1, collection.Count);
      Assert.IsNull(collection["name1"]);
      Assert.AreEqual(item, collection[0]);
      Assert.AreEqual(item, collection["name2"]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldRestrictAddingNulls()
    {
      new GridEntryCollection<CategoryItemMock>().Add(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ShouldRestrictTwoCategoriesWithSameNames()
    {
      GridEntryCollection<CategoryItemMock> collection = new GridEntryCollection<CategoryItemMock>();
      collection.Add(new CategoryItemMock("one"));
      collection.Add(new CategoryItemMock("one"));
    }
                
    [TestMethod]
    public void ShouldPerformBinarySearch()
    {
      CategoryItemMock item1 = new CategoryItemMock("a");
      CategoryItemMock item2 = new CategoryItemMock("b");

      GridEntryCollection<CategoryItemMock> list = new GridEntryCollection<CategoryItemMock>();
      list.Add(item1);
      list.Add(item2);

      int result = list.BinarySearch(item1);
      Assert.AreEqual<int>(0, result);

      result = list.BinarySearch(item2);
      Assert.AreEqual<int>(1, result);
    }

    [TestMethod]
    public void ShouldPerformBinarySearchWithComparer()
    {
      CategoryItemMock item1 = new CategoryItemMock("a");
      CategoryItemMock item2 = new CategoryItemMock("b");

      GridEntryCollection<CategoryItemMock> list = new GridEntryCollection<CategoryItemMock>();
      list.Add(item1);
      list.Add(item2);

      Assert.AreEqual<int>(0, list.BinarySearch(item1, new CategoryItemMockComparer()));
      Assert.AreEqual<int>(1, list.BinarySearch(item2, new CategoryItemMockComparer()));
    }

    [TestMethod]
    public void ShouldPerformItemsSort()
    {
      CategoryItemMock item1 = new CategoryItemMock("a");
      CategoryItemMock item2 = new CategoryItemMock("b");

      GridEntryCollection<CategoryItemMock> list = new GridEntryCollection<CategoryItemMock>();
      list.Add(item2);
      list.Add(item1);

      list.Sort(new CategoryItemMockComparer());

      Assert.AreEqual(item1, list[0]);
      Assert.AreEqual(item2, list[1]);
    }

    [TestMethod]
    public void ShouldRaiseChangeNotificationsOnSort()
    {
      bool propertyChanged = false;
      bool collectionChanged = false;

      GridEntryCollection<CategoryItemMock> list = new GridEntryCollection<CategoryItemMock>();

      (list as INotifyPropertyChanged).PropertyChanged += delegate { propertyChanged = true; };
      list.CollectionChanged += delegate { collectionChanged = true; };

      list.Sort(null);
      Assert.IsTrue(propertyChanged);
      Assert.IsTrue(collectionChanged);
    }
  }
}
