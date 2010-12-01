using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class NullValueConverterTest
  {
    [TestMethod]
    public void ShouldReturnNullStringForNullValue()
    {
      object converted = new NullValueConverter().Convert(null, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>(NullValueConverter.NullString, (string)converted);
    }

    [TestMethod]
    public void ShouldConvertProvidedValue()
    {
      object converted = new NullValueConverter().Convert("test", null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("test", (string)converted);
    }

    [TestMethod]
    public void ShouldConvertBackNull()
    {
      object converted = new NullValueConverter().ConvertBack(null, null, null, null);

      Assert.IsNull(converted);
    }

    [TestMethod]
    public void ShouldConvertBackEmptyString()
    {
      object converted = new NullValueConverter().ConvertBack(string.Empty, null, null, null);

      Assert.IsNull(converted);
    }

    [TestMethod]
    public void ShouldConvertBackNullString()
    {
      object converted = new NullValueConverter().ConvertBack(NullValueConverter.NullString, null, null, null);
      Assert.IsNull(converted);
    }

    [TestMethod]
    public void ShouldConvertBackProvidedValue()
    {
      object converted = new NullValueConverter().ConvertBack("test", null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("test", (string)converted);
    }

    [TestMethod]
    public void ShouldConvertNullToParameterValue()
    {
      object converted = new NullValueConverter().Convert(null, null, "<null>", null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual("<null>", converted);
    }

    [TestMethod]
    public void ShouldConvertBackNullFromParameter()
    {
      Assert.IsNull(new NullValueConverter().ConvertBack("<null>", null, "<null>", null));
    }
  }
}
