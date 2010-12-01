using System.ComponentModel;

namespace System.Windows.Controls.WpfPropertyGrid.Controls
{
  /// <summary>
  /// Combobox control to present enumeration classes.
  /// </summary>
  public class EnumDropdown : ComboBox
  {
    #region Fields
    private bool _wrappedEvents; 
    #endregion

    #region PropertyValue property
    /// <summary>
    /// Identifies the <see cref="PropertyValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PropertyValueProperty =
      DependencyProperty.Register("PropertyValue", typeof(PropertyItemValue), typeof(EnumDropdown),
        new PropertyMetadata(null, OnPropertyValueChanged));
    
    /// <summary>
    /// Gets or sets the property value. This is a dependency property.
    /// </summary>
    /// <value>The property value.</value>
    public PropertyItemValue PropertyValue
    {
      get { return (PropertyItemValue)GetValue(PropertyValueProperty); }
      set { SetValue(PropertyValueProperty, value); }
    }

    private static void OnPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var dropdown = (EnumDropdown)sender;
      if (e.OldValue != null) dropdown.UnwrapEventHandlers((PropertyItemValue)e.OldValue);

      var newValue = e.NewValue as PropertyItemValue;
      if (newValue == null) return;

      dropdown.SelectedValue = newValue.Value;
      dropdown.ItemsSource = newValue.ParentProperty.StandardValues;
      dropdown.WrapEventHandlers(newValue);
    } 
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumDropdown"/> class.
    /// </summary>
    public EnumDropdown()
    {
      IsSynchronizedWithCurrentItem = false;
      Loaded += EnumDropdownLoaded;
      Unloaded += EnumDropdownUnloaded;
    }
          
    void EnumDropdownLoaded(object sender, RoutedEventArgs e)
    {
      var value = PropertyValue;

      if (value != null)
        WrapEventHandlers(value);
    }

    void EnumDropdownUnloaded(object sender, RoutedEventArgs e)
    {
      var value = PropertyValue;

      if (value != null)
        UnwrapEventHandlers(value);
    }

    private void WrapEventHandlers(PropertyItemValue target)
    {
      if (target == null) return;
      if (_wrappedEvents) return;

      target.PropertyChanged += ValuePropertyChanged;
      _wrappedEvents = true;
    }

    // TODO: Provide a dedicated ValueChanged event not to listen to everything (performance increase)
    void ValuePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Value")
      {
        if (SelectedValue != PropertyValue.Value)
          SelectedValue = PropertyValue.Value;
      }
    }

    private void UnwrapEventHandlers(PropertyItemValue target)
    {
      if (target == null) return;
      if (!_wrappedEvents) return;

      target.PropertyChanged -= ValuePropertyChanged;
      _wrappedEvents = false;
    }

    /// <summary>
    /// Responds to a <see cref="T:System.Windows.Controls.ComboBox"/> selection change by raising a <see cref="E:System.Windows.Controls.Primitives.Selector.SelectionChanged"/> event.
    /// </summary>
    /// <param name="e">Provides data for <see cref="T:System.Windows.Controls.SelectionChangedEventArgs"/>.</param>
    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      // TODO: unsafe code!
      PropertyValue.Value = e.AddedItems[0];
      base.OnSelectionChanged(e);
    }
  }
}
