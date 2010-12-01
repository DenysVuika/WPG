using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace WpfPropertyGrid.UnitTests.Converters
{
  [TestClass]
  public class MultiBooleanToVisibilityConverterTest
  {
    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void DoesNotSupportBackwardConversion()
    {
      new MultiBooleanToVisibilityConverter().ConvertBack(null, null, null, null);
    }

    [TestMethod]
    public void ShouldConvertMultipleValues()
    {
      Assert.AreEqual(Visibility.Visible, new MultiBooleanToVisibilityConverter().Convert(new object[] { true, true, true }, null, null, null));
      Assert.AreEqual(Visibility.Collapsed, new MultiBooleanToVisibilityConverter().Convert(new object[] { false, false, false}, null, null, null));
      Assert.AreEqual(Visibility.Collapsed, new MultiBooleanToVisibilityConverter().Convert(new object[] { true, false }, null, null, null));
    }
  }
}
