using System.Windows;
using System.Windows.Controls.WpfPropertyGrid.Design;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class PropertyNameTextBlockTest
  {
    [TestMethod]
    public void ShouldAssignDefaultPropertyValues()
    {      
      PropertyNameTextBlock nameblock = new PropertyNameTextBlock();
      Assert.AreEqual<TextTrimming>(TextTrimming.CharacterEllipsis, nameblock.TextTrimming);
      Assert.AreEqual<TextWrapping>(TextWrapping.NoWrap, nameblock.TextWrapping);
      Assert.AreEqual<TextAlignment>(TextAlignment.Right, nameblock.TextAlignment);
      Assert.AreEqual<VerticalAlignment>(VerticalAlignment.Center, nameblock.VerticalAlignment);
      Assert.IsTrue(nameblock.ClipToBounds);
      Assert.IsFalse(KeyboardNavigation.GetIsTabStop(nameblock));
    }
  }
}
