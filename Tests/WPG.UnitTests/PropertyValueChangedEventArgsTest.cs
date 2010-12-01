using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class PropertyValueChangedEventArgsTest
  {
    private class PropertyItemTestCase : PropertyItem
    {
      public PropertyItemTestCase() : base(null) { }
    }

    [TestMethod]
    [Ignore]
    public void ShouldInitializeProperties()
    {
      PropertyItem oldValue = new PropertyItemTestCase();
      PropertyItem newValue = new PropertyItemTestCase();

      PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs(PropertyGrid.PropertyValueChangedEvent, newValue, oldValue);

      Assert.AreEqual<RoutedEvent>(PropertyGrid.PropertyValueChangedEvent, args.RoutedEvent);
      Assert.AreEqual<PropertyItem>(newValue, args.Property);
      Assert.AreEqual<PropertyItem>(oldValue, args.OldValue as PropertyItem);
    }
  }
}
