using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.PropertyEditing
{
  [TestClass]
  public class CategoryItemComparerTest
  {
    [TestMethod]
    public void ComparesNullToNull()
    {
      int result = new CategoryItemComparer().Compare(null, null);
      Assert.AreEqual<int>(0, result);
    }

    [TestMethod]
    public void ComparesNullToValue()
    {
      int result = new CategoryItemComparer().Compare(null, new CategoryItem(new PropertyGrid(), "name"));
      Assert.AreEqual<int>(-1, result);
    }

    [TestMethod]
    public void ComparesValueToNull()
    {
      int result = new CategoryItemComparer().Compare(new CategoryItem(new PropertyGrid(), "name"), null);
      Assert.AreEqual<int>(1, result);
    }

    [TestMethod]
    public void ComparesValuesByNames()
    {
      Assert.AreEqual<int>(0, new CategoryItemComparer().Compare(new CategoryItem(new PropertyGrid(), "name"), new CategoryItem(new PropertyGrid(), "name")));
      Assert.AreEqual<int>(-1, new CategoryItemComparer().Compare(new CategoryItem(new PropertyGrid(), "a"), new CategoryItem(new PropertyGrid(), "b")));
      Assert.AreEqual<int>(1, new CategoryItemComparer().Compare(new CategoryItem(new PropertyGrid(), "b"), new CategoryItem(new PropertyGrid(), "a")));
    }
  }
}
