using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class ExpanderHeaderWidthConverterTest
  {
    [TestMethod]
    public void ShouldReturnValueWithDefaultOffset()
    {
      object converted = new ExpanderHeaderWidthConverter().Convert(100.0, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(100 + ExpanderHeaderWidthConverter.DefaultOffset, (double)converted);
    }

    [TestMethod]
    public void ShouldReturnValueWithCustomOffset()
    {
      object converted = new ExpanderHeaderWidthConverter().Convert(100.0, null, 20.0, null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(120.0, (double)converted);
    }

    [TestMethod]
    public void ShouldTakeDefaultOffsetForWrongParameter()
    {
      object converted = new ExpanderHeaderWidthConverter().Convert(100.0, null, "wrong", null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(100 + ExpanderHeaderWidthConverter.DefaultOffset, (double)converted);
    }

    [TestMethod]
    public void ShouldConvertBackWithDefaultOffset()
    {
      object converted = new ExpanderHeaderWidthConverter().ConvertBack(100.0, null, null, null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(100 - ExpanderHeaderWidthConverter.DefaultOffset, (double)converted);
    }

    [TestMethod]
    public void ShouldConvertBackWithCustomOffset()
    {
      object converted = new ExpanderHeaderWidthConverter().ConvertBack(100.0, null, 50, null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(50, (double)converted); 
    }

    [TestMethod]
    public void ShouldConvertBackWithDefaultOffsetForWrongParameter()
    {
      object converted = new ExpanderHeaderWidthConverter().ConvertBack(100.0, null, "wrong", null);

      Assert.IsInstanceOfType(converted, typeof(double));
      Assert.AreEqual<double>(100 - ExpanderHeaderWidthConverter.DefaultOffset, (double)converted);
    }
  }
}
