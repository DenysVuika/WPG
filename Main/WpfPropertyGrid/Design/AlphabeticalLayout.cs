namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// The default alphabetical view for properties.
  /// </summary>
  public class AlphabeticalLayout : Control
  {
    static AlphabeticalLayout()
    {      
      DefaultStyleKeyProperty.OverrideMetadata(typeof(AlphabeticalLayout), new FrameworkPropertyMetadata(typeof(AlphabeticalLayout)));
    }
  }
}
