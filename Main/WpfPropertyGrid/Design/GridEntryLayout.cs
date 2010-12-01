using System.Windows.Data;

namespace System.Windows.Controls.WpfPropertyGrid.Design
{
  /// <summary>
  /// Defines a basement for GridEntry UI layouts (panels, lists, etc)
  /// </summary>
  /// <typeparam name="T">The type of elements in the control.</typeparam>
  public abstract class GridEntryLayout<T> : ItemsControl where T: GridEntryContainer, new()
  {
    /// <summary>
    /// Creates or identifies the element that is used to display the given item.
    /// </summary>
    /// <returns>
    /// The element that is used to display the given item.
    /// </returns>
    protected override DependencyObject GetContainerForItemOverride()
    {
      return new T();
    }

    /// <summary>
    /// Prepares the specified element to display the specified item.
    /// </summary>
    /// <param name="element">Element used to display the specified item.</param>
    /// <param name="item">Specified item.</param>
    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      var container = element as T;
      if (container != null)
      {
        container.DataContext = item;
        container.SetBinding(GridEntryContainer.EntryProperty, new Binding());
      }
      base.PrepareContainerForItemOverride(element, item);
    }
  }
}
