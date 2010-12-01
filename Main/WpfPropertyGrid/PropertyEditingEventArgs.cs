using System.ComponentModel;

namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Encapsulates a method that contains an object and PropertyEditingEventArgs event arguments.
  /// </summary>
  public delegate void PropertyEditingEventHandler(object sender, PropertyEditingEventArgs e);

  public class PropertyEditingEventArgs : RoutedEventArgs
  {
    /// <summary>
    /// Gets property descriptor.
    /// </summary>
    // TODO: Replace with my wrapper?
    public PropertyDescriptor PropertyDescriptor { get; private set; }

    public PropertyEditingEventArgs(RoutedEvent routedEvent, object source, PropertyDescriptor propertyDescriptor)
      : base(routedEvent, source)
    {
      PropertyDescriptor = propertyDescriptor;
    }
  }
}
