using System;
using System.Windows.Controls.WpfPropertyGrid;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Data
{
  [TestClass]
  public class NamedColorTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyName()
    {
      new NamedColor(null, Colors.AliceBlue);
    }

    [TestMethod]
    public void ShouldInitializeProperties()
    {
      NamedColor color = new NamedColor("color", Colors.AliceBlue);
      
      Assert.AreEqual<string>("color", color.Name);
      Assert.AreEqual<Color>(Colors.AliceBlue, color.Color);
      Assert.IsNotNull(color.Brush);
    }

    [TestMethod]
    public void ShouldFreezeBrush()
    {
      NamedColor color = new NamedColor("color", Colors.AliceBlue);
      Assert.IsTrue(color.Brush.IsFrozen);
    }

    [TestMethod]
    public void ShouldReturnNameWhenConvertedToString()
    {
      NamedColor color = new NamedColor("name", Colors.AliceBlue);
      Assert.AreEqual<string>("name", color.ToString());
    }
  }
}
