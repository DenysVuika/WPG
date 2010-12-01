using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.PropertyEditing
{
  [TestClass]
  public class PropertyItemComparerTest
  {
    private class PropertyItemMock : PropertyItem
    {
      public int Order { get; set; }
            
      public PropertyItemMock(string propertyName)
        : base(null)
      {
        this.Order = -1;
        this.Name = propertyName;
      }

      public PropertyItemMock(int order)
        : base(null)
      {
        Order = order;
      }

      public override T GetAttribute<T>()
      {
        if (this.Order >= 0 && typeof(T).Equals(typeof(PropertyOrderAttribute)))
          return new PropertyOrderAttribute(this.Order) as T;

        return base.GetAttribute<T>();
      }
    }

    [TestMethod]
    public void ComparesNullToNull()
    {
      int result = new PropertyItemComparer().Compare(null, null);
      Assert.AreEqual<int>(0, result);
    }

    [TestMethod]
    public void ComparesNullToValue()
    {
      int result = new PropertyItemComparer().Compare(null, new PropertyItemMock(null));
      Assert.AreEqual<int>(-1, result);
    }

    [TestMethod]
    public void ComparesValueToNull()
    {
      int result = new PropertyItemComparer().Compare(new PropertyItemMock(null), null);
      Assert.AreEqual<int>(1, result);
    }

    [TestMethod]
    public void ComparesOrderedItems()
    {
      PropertyItemComparer comparer = new PropertyItemComparer();

      int result = comparer.Compare(new PropertyItemMock(0), new PropertyItemMock(0));
      Assert.AreEqual<int>(0, result);

      result = comparer.Compare(new PropertyItemMock(0), new PropertyItemMock(1));
      Assert.AreEqual<int>(-1, result);

      result = comparer.Compare(new PropertyItemMock(1), new PropertyItemMock(0));
      Assert.AreEqual<int>(1, result);

      result = comparer.Compare(new PropertyItemMock("a"), new PropertyItemMock("a"));
      Assert.AreEqual<int>(0, result);

      result = comparer.Compare(new PropertyItemMock("a"), new PropertyItemMock("b"));
      Assert.AreEqual<int>(-1, result);

      result = comparer.Compare(new PropertyItemMock("b"), new PropertyItemMock("a"));
      Assert.AreEqual<int>(1, result);
    }

    [TestMethod]
    public void ShouldPerformCaseInsensitiveComparison()
    {
      Assert.AreEqual<int>(0, new PropertyItemComparer().Compare(new PropertyItemMock("a"), new PropertyItemMock("A")));
    }
  }
}
