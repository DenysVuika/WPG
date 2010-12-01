using System.Globalization;
using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class ObjectServicesTest
  {
    [TestMethod]
    public void ShouldReturnCurrentCultureForCultureInvariantTypes()
    {
      Assert.AreEqual<CultureInfo>(CultureInfo.CurrentCulture, ObjectServices.GetSerializationCulture(typeof(string)));
      Assert.AreEqual<CultureInfo>(CultureInfo.CurrentCulture, ObjectServices.GetSerializationCulture(typeof(int)));
    }
        
    [TestMethod]
    public void ShouldReturnCurrentCultureForNullType()
    {
      Assert.AreEqual<CultureInfo>(CultureInfo.CurrentCulture, ObjectServices.GetSerializationCulture(null));
    }

    [TestMethod]
    public void ShouldReturnInvariantCulture()
    {
      Assert.AreEqual<CultureInfo>(CultureInfo.InvariantCulture, ObjectServices.GetSerializationCulture(typeof(Rect)));
      Assert.AreEqual<CultureInfo>(CultureInfo.InvariantCulture, ObjectServices.GetSerializationCulture(typeof(Geometry)));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullPropertyDescriptor()
    {
      ObjectServices.GetPropertyConverter(null);
    }

    [TestMethod]
    public void ShouldReturnStringConverterForSpecialProperties()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(ContentControl))["Content"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultStringConverter, ObjectServices.GetPropertyConverter(descriptor));

      descriptor = TypeDescriptor.GetProperties(typeof(HeaderedContentControl))["Header"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultStringConverter, ObjectServices.GetPropertyConverter(descriptor));

      descriptor = TypeDescriptor.GetProperties(typeof(FrameworkElement))["ToolTip"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultStringConverter, ObjectServices.GetPropertyConverter(descriptor));

      descriptor = TypeDescriptor.GetProperties(typeof(FrameworkElement))["Tag"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultStringConverter, ObjectServices.GetPropertyConverter(descriptor));
    }

    [TestMethod]
    public void ShouldDecorateFontStretchConverter()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(Control))["FontStretch"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultFontStretchConverterDecorator, ObjectServices.GetPropertyConverter(descriptor));
    }

    [TestMethod]
    public void ShouldDecorateFontStyleConverter()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(Control))["FontStyle"];
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultFontStyleConverterDecorator, ObjectServices.GetPropertyConverter(descriptor));
    }

    [TestMethod]
    public void ShoudlDecorateFontWeightConverter()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(Control))["FontWeight"];      
      Assert.AreEqual<TypeConverter>(ObjectServices.DefaultFontWeightConverterDecorator, ObjectServices.GetPropertyConverter(descriptor));
    }

    [TestMethod]
    public void ShouldReturnPropertyConverter()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(TextBox))["Text"];
      Assert.AreEqual<TypeConverter>(TypeDescriptor.GetConverter(typeof(string)), ObjectServices.GetPropertyConverter(descriptor));
    }
  }
}
