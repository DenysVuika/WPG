using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class PropertyOrderAttributeTest
  {
    [TestMethod]
    public void ShouldAssignOrder()
    {
      PropertyOrderAttribute attribute = new PropertyOrderAttribute(10);
      Assert.AreEqual<int>(10, attribute.Order);
    }
  }
}
