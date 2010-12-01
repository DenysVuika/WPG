using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class BrowsableCategoryAttributeTest
  {
    [TestMethod]
    public void ShouldApplyCategoryName()
    {
      BrowsableCategoryAttribute attribute = new BrowsableCategoryAttribute("category");
      Assert.AreEqual<string>("category", attribute.CategoryName);
    }

    [TestMethod]
    public void ShouldApplyBrowsableState()
    {
      BrowsableCategoryAttribute attribute = new BrowsableCategoryAttribute(true);
      Assert.IsTrue(attribute.Browsable);
    }

    [TestMethod]
    public void ShouldApplyDefaultWildcard()
    {
      BrowsableCategoryAttribute attribute = new BrowsableCategoryAttribute(true);
      Assert.AreEqual<string>(BrowsableCategoryAttribute.All, attribute.CategoryName);      
    }

    [TestMethod]
    public void ShouldReplaceNullWithWildcard()
    {
      BrowsableCategoryAttribute attribute = new BrowsableCategoryAttribute(null, true);
      Assert.AreEqual<string>(BrowsableCategoryAttribute.All, attribute.CategoryName);
    }

    [TestMethod]
    public void ShouldAssignBrowsableByDefault()
    {
      BrowsableCategoryAttribute attribute = new BrowsableCategoryAttribute("category");
      Assert.IsTrue(attribute.Browsable);
    }
  }
}
