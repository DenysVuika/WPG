namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Specifies a UI container for <see cref="GridEntry"/>
  /// </summary>
  public abstract class GridEntryContainer : ContentControl
  {
    private ResourceLocator _resourceLocator = new ResourceLocator();
    /// <summary>
    /// Gets or sets the resource locator.
    /// </summary>
    /// <value>The resource locator.</value>
    protected ResourceLocator ResourceLocator
    {
      get { return _resourceLocator; }
      set { _resourceLocator = value; }
    }

    #region ParentContainer (attached)
    /// <summary>
    /// Identifies a ParentContainer attached property.
    /// </summary>
    public static readonly DependencyProperty ParentContainerProperty =
      DependencyProperty.RegisterAttached("ParentContainer", typeof(GridEntryContainer), typeof(GridEntryContainer),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets the parent container.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <returns>Parent container</returns>
    public static GridEntryContainer GetParentContainer(DependencyObject dependencyObject)
    {
      if (dependencyObject == null) throw new ArgumentNullException("dependencyObject");
      return (GridEntryContainer)dependencyObject.GetValue(ParentContainerProperty);
    }

    /// <summary>
    /// Sets the parent container.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="value">The value.</param>
    public static void SetParentContainer(DependencyObject dependencyObject, GridEntryContainer value)
    {
      if (dependencyObject == null) throw new ArgumentNullException("dependencyObject");
      dependencyObject.SetValue(ParentContainerProperty, value);
    }
    #endregion

    #region Entry
    /// <summary>
    /// Identifies the <see cref="Entry"/> property. This is a dependency property.
    /// </summary>
    public static readonly DependencyProperty EntryProperty =
      DependencyProperty.Register("Entry", typeof(GridEntry), typeof(GridEntryContainer),
      new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets the entry container displays.
    /// </summary>
    /// <value>The entry container diplays.</value>
    public GridEntry Entry
    {
      get { return (GridEntry)GetValue(EntryProperty); }
      set { SetValue(EntryProperty, value); }
    } 
    #endregion

    /// <summary>
    /// Gets the editor template to present contained entry.
    /// </summary>
    /// <value>The editor template to present contained entry.</value>
    public DataTemplate EditorTemplate
    {
      get { return FindEditorTemplate(); }
    }

    /// <summary>
    /// Finds the editor template.
    /// </summary>
    /// <returns>DataTemplate the Editor should be applied.</returns>
    protected virtual DataTemplate FindEditorTemplate()
    {
      if (Entry == null) return null;

      var editor = Entry.Editor;

      if (editor == null) return null;

      var template = editor.InlineTemplate as DataTemplate;
      if (template != null) return template;

      return ResourceLocator.GetResource(editor.InlineTemplate) as DataTemplate;
    }  
  }
}
