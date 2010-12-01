using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.PropertyEditing
{
  [TestClass]
  public class ValueExceptionEventArgsTest
  {
    private class PropertyItemMock : PropertyItem
    {
      public PropertyItemMock() : base(null) { }
    }

    private class PropertyItemValueMock : PropertyItemValue
    {
      public PropertyItemValueMock() : base(new PropertyItemMock()) { }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyMessage()
    {
      new ValueExceptionEventArgs(null, new PropertyItemValueMock(), ValueExceptionSource.Get, new Exception());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullPropertyItemValue()
    {
      new ValueExceptionEventArgs("message", null, ValueExceptionSource.Get, new Exception());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForException()
    {
      new ValueExceptionEventArgs("message", new PropertyItemValueMock(), ValueExceptionSource.Get, null);
    }

    [TestMethod]
    [Ignore]
    public void ShouldAssignAllPropertiesFromConstructor()
    {
      PropertyItemValueMock value = new PropertyItemValueMock();
      Exception exception = new Exception();

      ValueExceptionEventArgs arguments = new ValueExceptionEventArgs("message", value, ValueExceptionSource.Get, exception);

      Assert.AreEqual<string>("message", arguments.Message);
      Assert.AreEqual<PropertyItemValue>(value, arguments.PropertyValue);
      Assert.AreEqual<ValueExceptionSource>(ValueExceptionSource.Get, arguments.Source);
      Assert.AreEqual<Exception>(exception, arguments.Exception);
    }
  }
}
