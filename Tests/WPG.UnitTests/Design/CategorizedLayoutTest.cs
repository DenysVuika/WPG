using System;
using System.Windows.Controls.WpfPropertyGrid.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Layout
{
  [TestClass]
  public class CategorizedLayoutTest
  {
    private class CategorizedLayoutMock : CategorizedLayout
    {
      public object GetDefaultStyleKey()
      {
        return this.DefaultStyleKey;
      }
    }

    [TestMethod]
    public void ShouldOverrideDefaultStyleKey()
    {
      Assert.AreEqual<Type>((Type)new CategorizedLayoutMock().GetDefaultStyleKey(), typeof(CategorizedLayout));
    }
  }
}
