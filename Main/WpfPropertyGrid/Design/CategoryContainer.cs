namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Specialized UI container for a category entry.
  /// </summary>
  public class CategoryContainer : GridEntryContainer
  {
    /// <summary>
    /// Initializes the <see cref="CategoryContainer"/> class.
    /// </summary>
    static CategoryContainer()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryContainer), new FrameworkPropertyMetadata(typeof(CategoryContainer)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryContainer"/> class.
    /// </summary>
    public CategoryContainer()
    {
      SetParentContainer(this, this);     
    }    
  }
}