namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// Native Tab item for <see cref="TabbedLayout"/>
  /// </summary>
  public partial class TabbedLayoutItem : TabItem
  {
    /// <summary>
    /// Initializes the <see cref="TabbedLayoutItem"/> class.
    /// </summary>
    static TabbedLayoutItem()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(TabbedLayoutItem), new FrameworkPropertyMetadata(typeof(TabbedLayoutItem)));
    }
        
    /// <summary>
    /// Identifies the <see cref="CanClose"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanCloseProperty =
      DependencyProperty.Register("CanClose", typeof(bool), typeof(TabbedLayoutItem), new PropertyMetadata(false));
    
    /// <summary>
    /// Gets or sets a value indicating whether this instance can close.
    /// </summary>
    /// <value><c>true</c> if this instance can close; otherwise, <c>false</c>.</value>
    public bool CanClose
    {
      get { return (bool)GetValue(CanCloseProperty); }
      set { SetValue(CanCloseProperty, value); }
    }
  }
}
