namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// The default categorized view for properties.
  /// </summary>
  public class CategorizedLayout : Control
  {
    static CategorizedLayout()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(CategorizedLayout), new FrameworkPropertyMetadata(typeof(CategorizedLayout)));
    }
  }
}
