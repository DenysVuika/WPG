using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Collections;
using System.Windows;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class PropertyItemTest
  {
    #region Mock Objects

    private class PropertyItemMock : PropertyItem
    {
      public PropertyItemMock() : base(null) { }
      public PropertyItemMock(PropertyItemValue value) : base(value) { }
    }

    private class TypeConverterMock : TypeConverter
    {
      public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
      {
        return true;
      }

      public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {
        return new StandardValuesCollection(new string[] { "one", "two", "three" });
      }
    }

    private class BusinessObject : INotifyPropertyChanged
    {
      private string _Name;
      [DisplayName("Login")]
      [Category("Profile")]
      [System.ComponentModel.Description("Profile name")]
      [Browsable(false)]
      [Localizable(true)]
      [DefaultValue("DefaultName")]
      [TypeConverter(typeof(TypeConverterMock))]
      public string Name
      {
        get { return _Name; }
        set 
        {
          if (_Name == value) return;
          _Name = value;
          OnPropertyChanged("Name");
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      [DisplayName("Advanced")]
      public string AdvancedProperty { get; set; }

      [System.ComponentModel.Description("")]
      public string ReadOnlyProperty { get; private set; }

      [DisplayName("Parenthesis")]
      [ParenthesizePropertyName(true)]
      public string ParenthesizedProperty { get; set; }

      [DisplayName("NoParenthesis")]
      [ParenthesizePropertyName(false)]
      public string NonParenthesizedProperty { get; set; }

      public List<string> CollectionProperty { get; private set; }

      #region INotifyPropertyChanged Members

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion

      public static PropertyDescriptor ExtractProperty(string propertyName)
      {
        return TypeDescriptor.GetProperties(typeof(BusinessObject))[propertyName];
      }
    }

    private class DependencyObjectMock : DependencyObject
    {
      public static readonly DependencyProperty ValidPropertyProperty =
        DependencyProperty.Register("ValidProperty", typeof(string), typeof(DependencyObjectMock), new PropertyMetadata(string.Empty));

      public string ValidProperty
      {
        get { return (string)GetValue(ValidPropertyProperty); }
        set { SetValue(ValidPropertyProperty, value); }
      }

      public static readonly DependencyProperty InvalidPropertyProperty =
        DependencyProperty.Register("InvalidProperty", typeof(string), typeof(DependencyObjectMock), new PropertyMetadata(string.Empty), ValidateProperty);

      private static bool ValidateProperty(object value)
      {
        string stringvalue = value as string;
        if (stringvalue != null)
          return stringvalue != "value";
          
        return true;
      }

      public string InvalidProperty
      {
        get { return (string)GetValue(InvalidPropertyProperty); }
        set { SetValue(InvalidPropertyProperty, value); }
      }
            
      public static PropertyDescriptor ExtractProperty(string propertyName)
      {
        return TypeDescriptor.GetProperties(typeof(DependencyObjectMock))[propertyName];
      }
    }

    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullOwner()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(PropertyItem))["Name"];
      new PropertyItem(null, new object(), descriptor);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailtForNullComponent()
    {
      PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(PropertyItem))["Name"];
      new PropertyItem(new PropertyGrid(), null, descriptor);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullDescriptor()
    {
      new PropertyItem(new PropertyGrid(), new object(), null);
    }

    [TestMethod]
    public void ShouldInitiliasePropertiesWithConstructor()
    {
      PropertyGrid owner = new PropertyGrid();
      BusinessObject component = new BusinessObject();
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");

      PropertyItem property = new PropertyItem(owner, component, descriptor);

      Assert.AreEqual<PropertyGrid>(owner, property.Owner);
      Assert.AreEqual<string>("Name", property.Name);
      Assert.AreEqual(component, property.Component);
      Assert.IsFalse(property.IsBrowsable);
      Assert.AreEqual<string>("Profile name", property.Description);
      Assert.AreEqual<string>("Profile", property.CategoryName);
      Assert.IsTrue(property.IsLocalizable);
      Assert.AreEqual<string>("Login", property.DisplayName);

      Assert.IsTrue(property.Attributes.Count >= 5);      
    }

    [TestMethod]
    public void ShouldProvideAccessToMetadataAttributes()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));

      Assert.IsInstanceOfType(property.Metadata["Browsable"], typeof(BrowsableAttribute));
      Assert.IsInstanceOfType(property.Metadata["Localizable"], typeof(LocalizableAttribute));
      Assert.IsInstanceOfType(property.Metadata["DisplayName"], typeof(DisplayNameAttribute));
      Assert.IsInstanceOfType(property.Metadata["Category"], typeof(CategoryAttribute));
    }

    [TestMethod]
    public void ShouldReturnNullForMissingMetadata()
    {      
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      Assert.IsNull(property.Metadata["missing"]);
    }

    [TestMethod]
    public void ShouldMonitorValueChanges()
    {
      BusinessObject component = new BusinessObject();
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");

      PropertyItem property = new PropertyItem(new PropertyGrid(), component, descriptor);
      
      bool valueChanged = false;
      property.PropertyChanged += (sender, e) => { if (e.PropertyName == "PropertyValue") valueChanged = true; };

      component.Name = "NewName";
      Assert.IsTrue(valueChanged);
      Assert.AreEqual("NewName", property.PropertyValue.Value);
    }

    [TestMethod]
    public void ShouldRaiseDisplayNameChangedNotification()
    {
      BusinessObject component = new BusinessObject();
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, descriptor);

      int numberRaised = 0;
      property.PropertyChanged += (sender, e) => { if (e.PropertyName == "DisplayName") numberRaised++; };

      property.DisplayName = "name1";
      property.DisplayName = "name1";

      Assert.AreEqual<int>(1, numberRaised);
      Assert.AreEqual<string>("name1", property.DisplayName);
    }

    [TestMethod]
    public void ShouldRaiseDescriptionChangedNotification()
    {
      BusinessObject component = new BusinessObject();
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, descriptor);

      int numberRaised = 0;
      property.PropertyChanged += (sender, e) => { if (e.PropertyName == "Description") numberRaised++; };

      property.Description = "value1";
      property.Description = "value1";

      Assert.AreEqual<int>(1, numberRaised);
      Assert.AreEqual<string>("value1", property.Description);
    }

    [TestMethod]
    public void ShouldRaiseIsReadonlyChangedNotification()
    {
      BusinessObject component = new BusinessObject();
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, descriptor);

      int numberRaised = 0;
      property.PropertyChanged += (sender, e) => { if (e.PropertyName == "IsReadOnly") numberRaised++; };

      bool newState = !property.IsReadOnly;

      property.IsReadOnly = newState;
      property.IsReadOnly = newState;

      Assert.AreEqual<int>(1, numberRaised);
      Assert.AreEqual<bool>(newState, property.IsReadOnly);
    }

    [TestMethod]
    public void ShouldExtractAdvancedPropertyState()
    {
      BusinessObject component = new BusinessObject();

      PropertyItem property = new PropertyItem(new PropertyGrid(), component, BusinessObject.ExtractProperty("Name"));
      Assert.IsFalse(property.IsAdvanced);

      property = new PropertyItem(new PropertyGrid(), component, BusinessObject.ExtractProperty("AdvancedProperty"));
      Assert.IsTrue(property.IsAdvanced);
    }

    [TestMethod]
    public void ShouldApplyPropertyReadonlyState()
    {
      PropertyItem property = new PropertyItem(
        new PropertyGrid(), 
        new BusinessObject(), 
        TypeDescriptor.GetProperties(typeof(BusinessObject))["ReadOnlyProperty"]);

      Assert.IsTrue(property.IsReadOnly);
    }

    [TestMethod]
    public void ShouldExtractPropertyTypeFromDescriptor()
    {
      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), descriptor);

      Assert.AreEqual<Type>(descriptor.PropertyType, property.PropertyType);
    }

    [TestMethod]
    public void ShouldReturnNullMetadataForMissingDescriptor()
    {
      Assert.IsNull(new PropertyItemMock().Metadata);
    }

    [TestMethod]
    public void ShouldReturnNullPropertyTypeForMissingDescriptor()
    {
      Assert.IsNull(new PropertyItemMock().PropertyType);
    }

    [TestMethod]
    public void ShouldReturnNullAttributesForMissingDescriptor()
    {
      Assert.IsNull(new PropertyItemMock().Attributes);
    }

    [TestMethod]
    public void ShouldReturnNullValueForMissingDescriptor()
    {
      Assert.IsNull(new PropertyItemMock().GetValue());
    }

    [TestMethod]
    public void ShouldReturnNullDisplayNameForMissingDescriptor()
    {
      Assert.IsNull(new PropertyItemMock().DisplayName);
    }

    [TestMethod]
    public void ShouldWrapDisplayNameWithParenthesis()
    {      
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("ParenthesizedProperty"));
      Assert.AreEqual<string>("(Parenthesis)", property.DisplayName);
    }

    [TestMethod]
    public void ShouldSkipParenthesizedAttribute()
    {      
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("NonParenthesizedProperty"));
      Assert.AreEqual<string>("NoParenthesis", property.DisplayName);
    }

    [TestMethod]
    public void ShouldReturnTooltipByDescription()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      Assert.AreEqual<string>("Profile name", property.ToolTip.ToString());

      property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("ReadOnlyProperty"));
      Assert.AreEqual<string>("ReadOnlyProperty", property.ToolTip.ToString());
    }

    [TestMethod]
    public void ShouldReturnTooltipByDisplayName()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("AdvancedProperty"));
      Assert.AreEqual<string>("Advanced", property.ToolTip.ToString());      
    }

    [TestMethod]
    public void ShouldAllowClearingValueAccordingToDescriptor()
    {
      BusinessObject component = new BusinessObject();

      PropertyDescriptor descriptor = BusinessObject.ExtractProperty("Name");
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, descriptor);

      Assert.IsTrue(property.CanClearValue);
      Assert.AreEqual<bool>(descriptor.CanResetValue(component), property.CanClearValue);

      descriptor = BusinessObject.ExtractProperty("ReadOnlyProperty");
      property = new PropertyItem(new PropertyGrid(), component, descriptor);

      Assert.IsFalse(property.CanClearValue);
      Assert.AreEqual<bool>(descriptor.CanResetValue(component), property.CanClearValue);
    }

    [TestMethod]
    public void ShouldApproveCollectionProperty()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("CollectionProperty"));
      Assert.IsTrue(property.IsCollection);
    }

    [TestMethod]
    public void ShouldExtractStandardValuesFromConverter()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      
      ICollection values = property.StandardValues;
      Assert.IsNotNull(values);
      Assert.AreEqual<int>(3, values.Count);

      List<string> stringValues = values.Cast<string>().ToList();            
      Assert.AreEqual("one", stringValues[0]);
      Assert.AreEqual("two", stringValues[1]);
      Assert.AreEqual("three", stringValues[2]);
    }

    [TestMethod]
    public void ShouldReturnEmptyCollectionForStandardValues()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("AdvancedProperty"));
      ICollection values = property.StandardValues;

      Assert.IsNotNull(values);
      Assert.AreEqual<int>(0, values.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    // This is a "verification" test untill the corresponding property is implemented
    public void ShouldFailGettingIsDefaultValueByDefault()
    {
      bool isDefault = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("AdvancedProperty")).IsDefaultValue;
    }

    [TestMethod]
    public void ShouldClearValue()
    {
      BusinessObject component = new BusinessObject() { Name = "TestName" };
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, BusinessObject.ExtractProperty("Name"));      
      
      Assert.AreEqual<string>("TestName", property.GetValue().ToString());

      property.ClearValue();

      Assert.AreEqual<string>("DefaultName", property.GetValue().ToString());
    }

    [TestMethod]
    public void ShouldNotifyOnValueCleared()
    {
      BusinessObject component = new BusinessObject() { Name = "TestName" };
      PropertyItem property = new PropertyItem(new PropertyGrid(), component, BusinessObject.ExtractProperty("Name"));
      bool cleared = false;
      property.PropertyChanged += (sender, e) => { if (e.PropertyName == "PropertyValue") cleared = true; };
      
      property.ClearValue();
      Assert.IsTrue(cleared);
    }

    [TestMethod]
    public void ShouldFailMatchingNullPredicate()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      Assert.IsFalse(property.MatchesPredicate(null));
    }

    [TestMethod]
    public void ShouldFailMatchingPredicateForMissingDisplayNameAndPropertyType()
    {
      PropertyItemMock property = new PropertyItemMock();
      Assert.IsFalse(property.MatchesPredicate(new PropertyFilterPredicate("test")));
    }

    [TestMethod]
    public void ShouldMatchFilterPredicateByDisplayName()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      Assert.IsTrue(property.MatchesPredicate(new PropertyFilterPredicate("Login")));
    }

    [TestMethod]
    public void ShouldMatchFilterPredicateByPropertyType()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("ReadOnlyProperty"));
      Assert.IsTrue(property.MatchesPredicate(new PropertyFilterPredicate("string")));
    }

    [TestMethod]
    public void ShouldApplyFilterForProperty()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      Assert.IsTrue(property.MatchesFilter);

      property.ApplyFilter(null);
      Assert.IsTrue(property.MatchesFilter);

      property.ApplyFilter(new PropertyFilter("missing"));
      Assert.IsFalse(property.MatchesFilter);

      property.ApplyFilter(new PropertyFilter("string"));
      Assert.IsTrue(property.MatchesFilter);
    }

    [TestMethod]
    public void ShouldNofityWhenFilterApplied()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      bool applied = false;
      property.FilterApplied += delegate { applied = true; };

      property.ApplyFilter(null);
      Assert.IsTrue(applied);
    }

    [TestMethod]
    [Ignore]
    public void ShouldReturnParentValue()
    {
      PropertyItemValue value = new PropertyItemValue(new PropertyItemMock());
      PropertyItem property = new PropertyItemMock(value);

      Assert.AreEqual(value, property.ParentValue);
    }

    //[TestMethod]
    //[ExpectedException(typeof(ArgumentNullException))]
    //public void ShouldFailSettingNullPropertySource()
    //{
    //  new PropertyItemMock().SetPropertySouce(null);      
    //}

    //[TestMethod]
    //public void ShouldAssignNewComponentWithNewSource()
    //{
    //  BusinessObject component = new BusinessObject();
    //  PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
    //  property.SetPropertySouce(component);

    //  Assert.AreEqual(component, property.Component);
    //}

    //[TestMethod]
    //public void ShouldNotifyValueChangeOnNewSource()
    //{
    //  BusinessObject component = new BusinessObject { Name = "NewName" };
    //  PropertyItem property = new PropertyItem(new PropertyGrid(), new BusinessObject(), BusinessObject.ExtractProperty("Name"));
      
    //  property.SetValue("OldName");
    //  Assert.AreEqual<string>("OldName", property.PropertyValue.Value.ToString());

    //  bool valueChanged = false;
    //  property.PropertyChanged += (sender, e) => { if (e.PropertyName == "PropertyValue") valueChanged = true; };

    //  property.SetPropertySouce(component);

    //  Assert.IsTrue(valueChanged);
    //  Assert.AreEqual<string>("NewName", property.GetValue().ToString());
    //}

    [TestMethod]
    public void ShouldPassValidationForDependencyProperty()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new DependencyObjectMock(), DependencyObjectMock.ExtractProperty("ValidProperty"));
      Assert.IsTrue(property.Validate("value"));
    }

    [TestMethod]
    public void ShouldFailValidationForDependencyProperty()
    {
      PropertyItem property = new PropertyItem(new PropertyGrid(), new DependencyObjectMock(), DependencyObjectMock.ExtractProperty("InvalidProperty"));
      Assert.IsFalse(property.Validate("value"));
    }
  }
}
