using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class FontStyleConverterDecoratorTest
  {
    [TestMethod]
    public void ShouldProvideStandardValuesSupported()
    {
      FontStyleConverterDecorator decorator = new FontStyleConverterDecorator();
      Assert.IsTrue(decorator.GetStandardValuesSupported());
      Assert.AreEqual<int>(3, decorator.GetStandardValues().Count);
    }
  }
}
