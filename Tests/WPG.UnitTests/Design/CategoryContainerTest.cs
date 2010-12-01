using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class CategoryContainerTest
  {
    private class CategoryContainerTestCase : CategoryContainer
    {
      public object GetDefaultStyleKeyValue()
      {
        return DefaultStyleKey;
      }
    }

    [TestMethod]
    public void ShouldOverrideDefaultStyle()
    {
      CategoryContainerTestCase container = new CategoryContainerTestCase();
      Assert.AreEqual(typeof(CategoryContainer), container.GetDefaultStyleKeyValue());
    }

    [TestMethod]
    public void ShouldAssignItselfAsParentContainer()
    {
      CategoryContainer container = new CategoryContainer();
      Assert.AreEqual(container, CategoryContainer.GetParentContainer(container));
    }
  }
}
