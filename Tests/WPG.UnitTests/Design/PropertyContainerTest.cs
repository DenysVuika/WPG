using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class PropertyContainerTest
  {
    private class PropertyContainerTestCase : PropertyContainer
    {
      public object GetDefaultStyleKeyValue()
      {
        return DefaultStyleKey;
      }
    }

    [TestMethod]
    public void ShouldOverrideDefaultStyle()
    {
      PropertyContainerTestCase container = new PropertyContainerTestCase();
      Assert.AreEqual(typeof(PropertyContainer), container.GetDefaultStyleKeyValue());
    }

    [TestMethod]
    public void ShouldAssignItselfAsParentContainer()
    {
      PropertyContainer container = new PropertyContainer();
      Assert.AreEqual(container, PropertyContainer.GetParentContainer(container));
    }
  }
}
