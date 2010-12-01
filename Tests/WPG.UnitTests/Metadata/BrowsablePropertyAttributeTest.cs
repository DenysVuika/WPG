using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class BrowsablePropertyAttributeTest
  {
    [TestMethod]
    public void ShouldApplyPropertyName()
    {
      BrowsablePropertyAttribute attribute = new BrowsablePropertyAttribute("property");
      Assert.AreEqual<string>("property", attribute.PropertyName);
    }

    [TestMethod]
    public void ShouldApplyBrowsableState()
    {
      BrowsablePropertyAttribute attribute = new BrowsablePropertyAttribute(true);
      Assert.IsTrue(attribute.Browsable);
    }

    [TestMethod]
    public void ShouldApplyWildcard()
    {
      BrowsablePropertyAttribute attribute = new BrowsablePropertyAttribute(false);
      Assert.AreEqual<string>(BrowsablePropertyAttribute.All, attribute.PropertyName);
    }

    [TestMethod]
    public void ShouldReplaceNullWithWildCard()
    {
      BrowsablePropertyAttribute attribute = new BrowsablePropertyAttribute(null);
      Assert.AreEqual<string>(BrowsablePropertyAttribute.All, attribute.PropertyName);
    }

    [TestMethod]
    public void ShouldAssignBrowsableByDefault()
    {
      BrowsablePropertyAttribute attribute = new BrowsablePropertyAttribute("property");
      Assert.IsTrue(attribute.Browsable);
    }
  }
}
