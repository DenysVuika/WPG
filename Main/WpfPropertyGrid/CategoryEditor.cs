namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Specifies the default category editor.
  /// </summary>
  public class CategoryEditor : Editor
  {
    private static readonly Type ThisType = typeof(CategoryEditor);

    /// <summary>
    /// Identifies the <see cref="DeclaringType"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DeclaringTypeProperty = 
      DependencyProperty.Register("DeclaringType", typeof(Type), ThisType);

    /// <summary>
    /// Gets or sets the declaring type. This is a dependency property.
    /// </summary>
    /// <value>The declaring type.</value>
    public Type DeclaringType
    {
      get { return (Type)GetValue(DeclaringTypeProperty); }
      set { SetValue(DeclaringTypeProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="CategoryName"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CategoryNameProperty = 
      DependencyProperty.Register("CategoryName", typeof(string), ThisType);

    /// <summary>
    /// Gets or sets the name of the category. This is a dependency property.
    /// </summary>
    /// <value>The name of the category.</value>
    public string CategoryName
    {
      get { return (string)GetValue(CategoryNameProperty); }
      set { SetValue(CategoryNameProperty, value); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryEditor"/> class.
    /// </summary>
    public CategoryEditor()
    {
      // Default empty constructor
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryEditor"/> class.
    /// </summary>
    /// <param name="declaringType">Declaring type.</param>
    /// <param name="categoryName">Name of the category.</param>
    /// <param name="inlineTemplate">The inline template.</param>
    public CategoryEditor(Type declaringType, string categoryName, object inlineTemplate)
    {
      if (declaringType == null) throw new ArgumentNullException("declaringType");
      if (string.IsNullOrEmpty(categoryName)) throw new ArgumentNullException("categoryName");

      DeclaringType = declaringType;
      CategoryName = categoryName;
      InlineTemplate = inlineTemplate;
    }
  }
}
