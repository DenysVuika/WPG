using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class FontWeightConverterDecoratorTest
  {
    [TestMethod]
    public void ShouldProvideStandardValuesSupported()
    {
      FontWeightConverterDecorator decorator = new FontWeightConverterDecorator();
      Assert.IsTrue(decorator.GetStandardValuesSupported());
      Assert.AreEqual<int>(10, decorator.GetStandardValues().Count);
    }
  }
}
