using System.Windows.Input;

namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  ///Provides a standard set of property editor related commands.
  /// </summary>
  public static class PropertyEditorCommands
  {
    private static readonly Type ThisType = typeof(PropertyEditorCommands);
        
    private static readonly RoutedUICommand _ShowDialogEditor = new RoutedUICommand("Show Dialog Editor", "ShowDialogEditorCommand", ThisType);
    private static readonly RoutedUICommand _ShowExtendedEditor = new RoutedUICommand("Show Extended Editor", "ShowExtendedEditorCommand", ThisType);

    /// <summary>
    /// Defines a command to show dialog editor for a property.
    /// </summary>    
    public static RoutedUICommand ShowDialogEditor
    {
      get { return _ShowDialogEditor; }
    }

    /// <summary>
    /// Defines a command to show extended editor for a property.
    /// </summary>
    public static RoutedUICommand ShowExtendedEditor
    {
      get { return _ShowExtendedEditor; }
    }
  }
}
