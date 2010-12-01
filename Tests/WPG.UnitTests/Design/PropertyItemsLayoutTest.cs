using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using System.Windows.Controls.WpfPropertyGrid.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class PropertyItemsLayoutTest
  {
    #region Mock Objects
    private class PropertyItemsLayoutMock : PropertyItemsLayout
    {
      public DependencyObject CallContainerForItemOverride()
      {
        return this.GetContainerForItemOverride();
      }

      public void CallPrepareContainerForItemOverride(DependencyObject element, object item)
      {
        this.PrepareContainerForItemOverride(element, item);
      }
    }

    private class GridEntryMock : GridEntry
    {
      public override void ApplyFilter(PropertyFilter filter)
      {
      }

      public override bool MatchesPredicate(PropertyFilterPredicate predicate)
      {
        return false;
      }
    }
    #endregion

    [TestMethod]
    public void ShouldProvideCategoryContainerForItemOverride()
    {
      PropertyItemsLayoutMock layout = new PropertyItemsLayoutMock();
      DependencyObject container = layout.CallContainerForItemOverride();

      Assert.IsNotNull(container);
      Assert.IsInstanceOfType(container, typeof(PropertyContainer));
    }

    [TestMethod]
    public void ShouldAssignDataContextWithProvidedItem()
    {
      PropertyContainer container = new PropertyContainer();
      GridEntry item = new GridEntryMock();

      new PropertyItemsLayoutMock().CallPrepareContainerForItemOverride(container, item);

      Assert.AreEqual(item, container.DataContext);
    }

    [TestMethod]
    public void ShouldAssignBindingWithProvidedItem()
    {
      PropertyContainer container = new PropertyContainer();
      GridEntry item = new GridEntryMock();

      new PropertyItemsLayoutMock().CallPrepareContainerForItemOverride(container, item);

      var expression = container.GetBindingExpression(GridEntryContainer.EntryProperty);
      Assert.IsNotNull(expression);
    }

    [TestMethod]
    public void ShouldNotFailPreparingNonCategoryContainer()
    {
      new PropertyItemsLayoutMock().CallPrepareContainerForItemOverride(new DependencyObject(), "item");
    }
  }
}
