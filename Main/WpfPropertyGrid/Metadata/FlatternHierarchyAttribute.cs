namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Specifies whether the property hierarchy should be flatterned for visualization.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
  [Obsolete("This attribute is deprecated and is no longer supported by PropertyGrid. Please consider using custom type descriptors or ModelView classes in order to gain flattern behavior.", false)]
  public sealed class FlatternHierarchyAttribute : Attribute { }
}
