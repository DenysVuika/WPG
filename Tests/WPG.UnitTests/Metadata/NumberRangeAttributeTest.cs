using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class NumberRangeAttributeTest
  {
    [TestMethod]
    public void ShouldAssignAllProperties()
    {
      NumberRangeAttribute attribute = new NumberRangeAttribute(10, 20, 30, 40);
      Assert.AreEqual<double>(10, attribute.Minimum);
      Assert.AreEqual<double>(20, attribute.Maximum);
      Assert.AreEqual<double>(30, attribute.Tick);
      Assert.AreEqual<double>(40, attribute.Precision);
    }

    [TestMethod]
    public void ShouldAssignPrecisionByDefault()
    {
      NumberRangeAttribute attribute = new NumberRangeAttribute(10, 20, 30);
      Assert.AreEqual<double>(0, attribute.Precision);
    }
  }
}
