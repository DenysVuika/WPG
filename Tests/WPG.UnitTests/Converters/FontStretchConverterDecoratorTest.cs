using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Globalization;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class FontStretchConverterDecoratorTest
  {
    [TestMethod]
    public void ShouldProvideStandardValuesSupported()
    {
      FontStretchConverterDecorator decorator = new FontStretchConverterDecorator();
      Assert.IsTrue(decorator.GetStandardValuesSupported());
      Assert.AreEqual<int>(9, decorator.GetStandardValues().Count);
    }

    [TestMethod]
    public void ShouldRedirectConversionCalls()
    {
      FontStretchConverter original = new FontStretchConverter();
      FontStretchConverterDecorator decorated = new FontStretchConverterDecorator();

      Assert.AreEqual<bool>(original.CanConvertFrom(null, typeof(string)), decorated.CanConvertFrom(null, typeof(string)));
      Assert.AreEqual<bool>(original.CanConvertTo(null, typeof(int)), decorated.CanConvertTo(null, typeof(int)));
      Assert.AreEqual(original.ConvertFrom(null, CultureInfo.InvariantCulture, "Condensed"), decorated.ConvertFrom(null, CultureInfo.InvariantCulture, "Condensed"));
      Assert.AreEqual(original.ConvertTo(null, CultureInfo.InvariantCulture, FontStretches.Condensed, typeof(string)), decorated.ConvertTo(null, CultureInfo.InvariantCulture, FontStretches.Condensed, typeof(string)));
    }
  }
}
