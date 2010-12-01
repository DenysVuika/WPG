using System.Windows.Input;

namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// Specifies a property name presenter.
  /// </summary>
  public sealed class PropertyNameTextBlock : TextBlock
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNameTextBlock"/> class.
    /// </summary>
    public PropertyNameTextBlock()
    {
     TextTrimming = TextTrimming.CharacterEllipsis;
     TextWrapping = TextWrapping.NoWrap;
     TextAlignment = TextAlignment.Right;
     VerticalAlignment = VerticalAlignment.Center;
     ClipToBounds = true;
      
      KeyboardNavigation.SetIsTabStop(this, false);
    }
  }
}
