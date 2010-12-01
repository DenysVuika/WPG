using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Collections.Generic;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class CategoryItemTest
  {
    #region Mock Objects

    private class CategoryItemMock : CategoryItem
    {
      public event EventHandler WasDisposed;
      private void OnDisposed()
      {
        EventHandler handler = WasDisposed;
        if (handler != null) handler(this, EventArgs.Empty);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          OnDisposed();
        }
        base.Dispose(disposing);
      }

      public CategoryItemMock() : base(new PropertyGrid(), new CategoryAttribute()) { }
    }

    private class PropertyItemMock : PropertyItem
    {
      public PropertyItemMock() : base(null) { }
      public PropertyItemMock(string name)
        : this()
      {
        base.Name = name;
        base.DisplayName = name;
      }
    }

    private class PropertyItemComparerMock : IComparer<PropertyItem>
    {
      #region IComparer<PropertyItem> Members

      public int Compare(PropertyItem x, PropertyItem y)
      {
        return string.Compare(y.Name, x.Name, true);
      }

      #endregion
    }

    private class PropertyGridMock : PropertyGrid
    {
      private Editor editor;

      public PropertyGridMock(Editor editor)
      {
        this.editor = editor;
      }

      public override Editor GetEditor(GridEntry entry)
      {
        return editor;
      }
    }
    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForMissingOwner()
    {
      new CategoryItem(null, "category");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForMissingCategoryName()
    {
      new CategoryItem(new PropertyGrid(), string.Empty);
    }

    [TestMethod]
    public void ShouldAssignPropertiesByConstructor()
    {
      PropertyGrid owner = new PropertyGrid();
      CategoryItem category = new CategoryItem(owner, "category");

      Assert.AreEqual<PropertyGrid>(owner, category.Owner);
      Assert.AreEqual<string>("category", category.Name);
    }

    [TestMethod]
    public void ShouldInitializeAgainstCategoryAttribute()
    {
      PropertyGrid owner = new PropertyGrid();
      CategoryAttribute attribute = new CategoryAttribute("category");
      CategoryItem category = new CategoryItem(owner, attribute);

      Assert.AreEqual<PropertyGrid>(owner, category.Owner);
      Assert.AreEqual<string>("category", category.Name);
      Assert.AreEqual<Attribute>(attribute, category.Attribute);
    }

    [TestMethod]
    public void ShouldInitializeAgainstEmtpyAttribute()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      Assert.AreEqual<string>(CategoryAttribute.Default.Category, category.Name);
    }

    [TestMethod]
    public void ShouldNotifyIsExpandedValueChange()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), "category");

      int eventsNumber = 0;
      category.PropertyChanged += (sender, e) => { if (e.PropertyName == "IsExpanded") eventsNumber++; };

      category.IsExpanded = !category.IsExpanded;
      Assert.AreEqual<int>(1, eventsNumber);
    }

    [TestMethod]
    public void ShouldNotifyIsExpandedValueOnlyOnce()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), "category");

      int eventsNumber = 0;
      category.PropertyChanged += (sender, e) => { if (e.PropertyName == "IsExpanded") eventsNumber++; };

      bool current = category.IsExpanded;

      category.IsExpanded = !current;
      category.IsExpanded = !current;

      Assert.AreEqual<int>(1, eventsNumber);
    }

    [TestMethod]
    public void ShouldNotifyIsBrowsableValueChanged()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());

      int numberRaised = 0;
      category.PropertyChanged += (sender, e) => { if (e.PropertyName == "IsBrowsable") numberRaised++; };
      category.BrowsableChanged += (sender, e) => numberRaised++;

      bool browsable = category.IsBrowsable;
      category.IsBrowsable = !browsable;
      category.IsBrowsable = !browsable;

      Assert.AreEqual<int>(2, numberRaised);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailAddingNullProperty()
    {
      new CategoryItem(new PropertyGrid(), new CategoryAttribute()).AddProperty(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailAddingNamelessProperty()
    {
      new CategoryItem(new PropertyGrid(), new CategoryAttribute()).AddProperty(new PropertyItemMock());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ShouldFailAddingDuplicateProperty()
    {
      PropertyItemMock property = new PropertyItemMock("property");
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());

      category.AddProperty(property);
      category.AddProperty(property);
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void ShouldFailAddingPropertyDirectly()
    {
      (new CategoryItem(new PropertyGrid(), new CategoryAttribute()).Properties as IList<PropertyItem>).Add(new PropertyItemMock());
    }

    [TestMethod]
    public void ShouldRetreivePropertyByName()
    {
      PropertyItemMock property1 = new PropertyItemMock("property1");
      PropertyItemMock property2 = new PropertyItemMock("property2");

      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(property1);
      category.AddProperty(property2);

      Assert.AreEqual<PropertyItem>(property1, category["property1"]);
      Assert.AreEqual<PropertyItem>(property2, category["property2"]);
    }

    [TestMethod]
    public void ShouldReturnNullForInvalidPropertyName()
    {
      Assert.IsNull(new CategoryItem(new PropertyGrid(), new CategoryAttribute())[null]);
    }

    [TestMethod]
    public void ShouldReturnNullForMissingPropertyName()
    {
      Assert.IsNull(new CategoryItem(new PropertyGrid(), new CategoryAttribute())["missing"]);
    }

    [TestMethod]
    public void ShouldOrderPropertiesUponAdding()
    {
      PropertyItem property1 = new PropertyItemMock("property1");
      PropertyItem property2 = new PropertyItemMock("property2");

      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(property2);
      category.AddProperty(property1);

      Assert.AreEqual<PropertyItem>(property1, category.Properties[0]);
      Assert.AreEqual<PropertyItem>(property2, category.Properties[1]);
    }

    [TestMethod]
    public void ShouldApplyHasVisiblePropertiesByAtLeastOneOccurrence()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      Assert.IsFalse(category.HasVisibleProperties);

      category.AddProperty(new PropertyItemMock("property1") { IsBrowsable = true });
      Assert.IsTrue(category.HasVisibleProperties);

      category.AddProperty(new PropertyItemMock("property2"));
      Assert.IsTrue(category.HasVisibleProperties);
    }

    [TestMethod]
    public void ShouldNotifyOnVisiblePropertiesChange()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      int raisedNumber = 0;
      category.PropertyChanged += (sender, e) => { if (e.PropertyName == "HasVisibleProperties") raisedNumber++; };
      
      category.AddProperty(new PropertyItemMock("property1"));
      category.AddProperty(new PropertyItemMock("property2") { IsBrowsable = true });
      category.AddProperty(new PropertyItemMock("property3") { IsBrowsable = true });

      Assert.AreEqual<int>(1, raisedNumber);
    }

    [TestMethod]
    public void ShouldListenToPropertyVisibilityChanges()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      PropertyItemMock property = new PropertyItemMock("property");
      category.AddProperty(property);
      Assert.IsFalse(category.HasVisibleProperties);

      property.IsBrowsable = true;
      Assert.IsTrue(category.HasVisibleProperties);

      property.IsBrowsable = false;
      Assert.IsFalse(category.HasVisibleProperties);
    }

    [TestMethod]
    public void ShouldNotifyComparerChange()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      int numberRaised = 0;
      category.PropertyChanged += (sender, e) => { if (e.PropertyName == "Comparer") numberRaised++; };

      PropertyItemComparer comparer = new PropertyItemComparer();

      category.Comparer = comparer;
      category.Comparer = comparer;

      Assert.AreEqual(comparer, category.Comparer);
      Assert.AreEqual<int>(1, numberRaised);
    }

    [TestMethod]
    public void ShouldSortUponSettingNewComparer()
    {
      PropertyItem property1 = new PropertyItemMock("property1");
      PropertyItem property2 = new PropertyItemMock("property2");

      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(property2);
      category.AddProperty(property1);

      Assert.AreEqual<PropertyItem>(property1, category.Properties[0]);
      Assert.AreEqual<PropertyItem>(property2, category.Properties[1]);

      category.Comparer = new PropertyItemComparerMock();

      Assert.AreEqual<PropertyItem>(property2, category.Properties[0]);
      Assert.AreEqual<PropertyItem>(property1, category.Properties[1]);
    }

    [TestMethod]
    public void ShouldRedirectFilterMatchToProperties()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(new PropertyItemMock("property1"));
      category.AddProperty(new PropertyItemMock("property2"));

      Assert.IsTrue(category.MatchesPredicate(new PropertyFilterPredicate("property")));
      Assert.IsFalse(category.MatchesPredicate(new PropertyFilterPredicate("missing")));
    }

    [TestMethod]
    public void ShouldApplyFilterToPropertiesContained()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(new PropertyItemMock("property1") { IsBrowsable = true });
      category.AddProperty(new PropertyItemMock("property2") { IsBrowsable = true });

      category.ApplyFilter(new PropertyFilter("missing"));
      Assert.IsFalse(category.MatchesFilter);
      Assert.IsFalse(category.HasVisibleProperties);

      category.ApplyFilter(new PropertyFilter("property"));
      Assert.IsTrue(category.MatchesFilter);
      Assert.IsTrue(category.HasVisibleProperties);
    }

    [TestMethod]
    public void ShouldExpandOnAppliedFilterMatch()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(new PropertyItemMock("property1") { IsBrowsable = true });
      category.AddProperty(new PropertyItemMock("property2") { IsBrowsable = true });
      category.IsExpanded = false;

      category.ApplyFilter(new PropertyFilter("property"));
      Assert.IsTrue(category.MatchesFilter);
      Assert.IsTrue(category.IsExpanded);
    }

    [TestMethod]
    public void ShouldNotifyFilterApplied()
    {
      CategoryItem category = new CategoryItem(new PropertyGrid(), new CategoryAttribute());
      category.AddProperty(new PropertyItemMock("property1") { IsBrowsable = true });
      category.AddProperty(new PropertyItemMock("property2") { IsBrowsable = true });

      bool filterApplied = false;
      category.FilterApplied += delegate { filterApplied = true; };
      category.ApplyFilter(new PropertyFilter("property"));

      Assert.IsTrue(filterApplied);
    }

    [TestMethod]
    public void ShouldResolveEditor()
    {
      Editor editor = new CategoryEditor();
      CategoryItem category = new CategoryItem(new PropertyGridMock(editor), new CategoryAttribute());

      Assert.AreEqual<Editor>(editor, category.Editor);
    }

    [TestMethod]
    public void ShouldBeDisposed()
    {
      bool disposed = false;

      using (var category = new CategoryItemMock())
      {
        category.WasDisposed += (sender, e) => disposed = true;
      }

      Assert.IsTrue(disposed);
    }
  }
}
