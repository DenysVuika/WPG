using System.Windows.Data;

namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// Defines a content presenter control for a Property editor.
  /// </summary>
  public sealed class PropertyEditorContentPresenter : ContentPresenter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyEditorContentPresenter"/> class.
    /// </summary>
    public PropertyEditorContentPresenter()
    {
      var contentBinding = new Binding
      {
        RelativeSource = RelativeSource.Self,
        Path = new PropertyPath("(0).(1).PropertyValue", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
      };

      var contentTemplateBinding = new Binding
      {
        RelativeSource = RelativeSource.Self,
        Path = new PropertyPath("(0).EditorTemplate", new[] { GridEntryContainer.ParentContainerProperty })
      };

      SetBinding(ContentProperty, contentBinding);
      SetBinding(ContentTemplateProperty, contentTemplateBinding); 
    }
  }
}
