using System.Windows.Data;

namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// Defines a content presenter control for a Category editor.
  /// </summary>
  public class CategoryEditorContentPresenter : ContentPresenter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryEditorContentPresenter"/> class.
    /// </summary>
    public CategoryEditorContentPresenter()
    {
      var contentBinding = new Binding
      {
        RelativeSource = RelativeSource.Self,
        Path = new PropertyPath("(0).(1)", new[] { GridEntryContainer.ParentContainerProperty, GridEntryContainer.EntryProperty })
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
