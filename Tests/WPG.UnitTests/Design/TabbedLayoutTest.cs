using System;
using System.Windows.Controls.WpfPropertyGrid.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Layout
{
  [TestClass]
  public class TabbedLayoutTest
  {
    private class TabbedLayoutMock : TabbedLayout
    {
      public object GetDefaultStyleKey()
      {
        return this.DefaultStyleKey;
      }
    }

    [TestMethod]
    public void ShouldOverrideDefaultStyleKey()
    {
      Assert.AreEqual<Type>((Type)new TabbedLayoutMock().GetDefaultStyleKey(), typeof(TabbedLayout));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailSettingCanCloseForNull()
    {
      TabbedLayoutMock.SetCanClose(null, true);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingCanCloseForNull()
    {
      TabbedLayoutMock.GetCanClose(null);
    }

    [TestMethod]
    public void ShouldSetCanCloseProperly()
    {
      TabbedLayoutMock target = new TabbedLayoutMock();
      
      TabbedLayoutMock.SetCanClose(target, true);
      Assert.AreEqual<bool>(true, TabbedLayoutMock.GetCanClose(target));

      TabbedLayoutMock.SetCanClose(target, false);
      Assert.AreEqual<bool>(false, TabbedLayoutMock.GetCanClose(target));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailSettingHeaderForNull()
    {
      TabbedLayoutMock.SetHeader(null, "header");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingHeaderFromNull()
    {
      TabbedLayoutMock.GetHeader(null);
    }

    [TestMethod]
    public void ShouldReplaceNullHeaderWithDefault()
    {
      TabbedLayoutMock target = new TabbedLayoutMock();
      
      TabbedLayoutMock.SetHeader(target, "header");
      Assert.AreEqual<string>("header", TabbedLayoutMock.GetHeader(target));

      TabbedLayoutMock.SetHeader(target, null);
      Assert.AreEqual<string>(TabbedLayoutMock.DefaultHeader, TabbedLayoutMock.GetHeader(target));
    }
  }
}
