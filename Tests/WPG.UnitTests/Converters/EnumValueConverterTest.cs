using System;
using System.Globalization;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class EnumValueConverterTest
  {
    private enum EnumTestCase
    {      
      One,
      [System.ComponentModel.Description("description")]
      Two,
      [System.ComponentModel.Description()]
      Three,
    }

    [TestMethod]
    public void ShouldReturnNullValueString()
    {
      object converted = new EnumValueConverter().Convert(null, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>(EnumValueConverter.NullValueString, (string)converted);
    }

    [TestMethod]
    public void ShoudConvertEnumToString()
    {
      object converted = new EnumValueConverter().Convert(EnumTestCase.One, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("One", (string)converted);
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void ShouldFailPerformingConversionBack()
    {
      new EnumValueConverter().ConvertBack(null, null, null, CultureInfo.InvariantCulture);
    }

    [TestMethod]
    public void ShouldTakeDescriptionAttributeDuringConversion()
    {
      object converted = new EnumValueConverter().Convert(EnumTestCase.Two, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("description", (string)converted);
    }

    [TestMethod]
    public void ShouldConvertToStringForEmptyDescriptionAttribute()
    {
      object converted = new EnumValueConverter().Convert(EnumTestCase.Three, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("Three", (string)converted);
    }

    [TestMethod]
    public void ShouldConvertNonEnumValueToString()
    {
      object converted = new EnumValueConverter().Convert(1234, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(string));
      Assert.AreEqual<string>("1234", (string)converted);
    }
  }
}
