using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace System.Windows.Controls.WpfPropertyGrid.Controls
{
  /// <summary>
  /// Base class for Slider-like editor implementations.
  /// </summary>
  public abstract class SliderEditorBase : Slider
  {
    private static readonly Type ThisType = typeof(SliderEditorBase);

    #region PropertyEditingStarted Event

    /// <summary>
    /// Identifies the <see cref="PropertyEditingStarted"/> routed event.
    /// </summary>
    public static readonly RoutedEvent PropertyEditingStartedEvent = PropertyGrid.PropertyEditingStartedEvent.AddOwner(ThisType);

    /// <summary>
    /// Occurs when property editing started.
    /// </summary>
    public event PropertyEditingEventHandler PropertyEditingStarted
    {
      add { AddHandler(PropertyEditingStartedEvent, value); }
      remove { RemoveHandler(PropertyEditingFinishedEvent, value); }
    }

    #endregion PropertyEditingStarted Event
    
    #region PropertyEditingFinished Event

    /// <summary>
    /// Identifies the <see cref="PropertyEditingFinished"/> routed event.
    /// </summary>
    public static readonly RoutedEvent PropertyEditingFinishedEvent = PropertyGrid.PropertyEditingFinishedEvent.AddOwner(ThisType);

    /// <summary>
    /// Occurs when property editing is finished.
    /// </summary>
    public event PropertyEditingEventHandler PropertyEditingFinished
    {
      add { AddHandler(PropertyEditingFinishedEvent, value); }
      remove { RemoveHandler(PropertyEditingFinishedEvent, value); }
    } 

    #endregion

    #region PropertyDescriptor Property
    
    /// <summary>
    /// Identifies the <see cref="PropertyDescriptor"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PropertyDescriptorProperty =
      DependencyProperty.Register("PropertyDescriptor", typeof(PropertyDescriptor), ThisType);

    /// <summary>
    /// Underlying property descriptor. This is a dependency property.
    /// </summary>
    public PropertyDescriptor PropertyDescriptor
    {
      get { return (PropertyDescriptor)GetValue(PropertyDescriptorProperty); }
      set { SetValue(PropertyDescriptorProperty, value); }
    }

    #endregion PropertyDescriptor Property

    protected override void OnThumbDragStarted(DragStartedEventArgs e)
    {
      OnPropertyEditingStarted();
      base.OnThumbDragStarted(e);
    }

    protected override void OnThumbDragCompleted(DragCompletedEventArgs e)
    {
      OnPropertyEditingFinished();
      base.OnThumbDragCompleted(e);
    }

    /// <summary>
    /// Raises the <see cref="PropertyEditingStarted"/> event.
    /// </summary>
    protected virtual void OnPropertyEditingStarted()
    {
      RaiseEvent(new PropertyEditingEventArgs(PropertyEditingStartedEvent, this, PropertyDescriptor));
    }

    /// <summary>
    /// Raises the <see cref="PropertyEditingFinished"/> event.
    /// </summary>
    protected virtual void OnPropertyEditingFinished()
    {
      RaiseEvent(new PropertyEditingEventArgs(PropertyEditingFinishedEvent, this, PropertyDescriptor));
    }
  }
}
