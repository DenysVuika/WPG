using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class PropertyFilterAppliedEventArgsTest
  {
    [TestMethod]
    public void ShouldInitializeFilter()
    {
      PropertyFilter filter = new PropertyFilter("test");
      PropertyFilterAppliedEventArgs args = new PropertyFilterAppliedEventArgs(filter);

      Assert.AreEqual<PropertyFilter>(filter, args.Filter);
    }
  }
}
