using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using System.Windows.Controls.WpfPropertyGrid.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class CategoryItemsLayoutTest
  {
    #region Mock Objects
    private class CategoryItemsLayoutMock : CategoryItemsLayout
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
      CategoryItemsLayoutMock layout = new CategoryItemsLayoutMock();
      DependencyObject container = layout.CallContainerForItemOverride();
      
      Assert.IsNotNull(container);
      Assert.IsInstanceOfType(container, typeof(CategoryContainer));
    }

    [TestMethod]
    public void ShouldAssignDataContextWithProvidedItem()
    {
      CategoryContainer container = new CategoryContainer();
      GridEntry item = new GridEntryMock();

      new CategoryItemsLayoutMock().CallPrepareContainerForItemOverride(container, item);
            
      Assert.AreEqual(item, container.DataContext);
    }

    [TestMethod]
    public void ShouldAssignBindingWithProvidedItem()
    {
      CategoryContainer container = new CategoryContainer();
      GridEntry item = new GridEntryMock();

      new CategoryItemsLayoutMock().CallPrepareContainerForItemOverride(container, item);

      var expression = container.GetBindingExpression(GridEntryContainer.EntryProperty);
      Assert.IsNotNull(expression);
    }

    [TestMethod]
    public void ShouldNotFailPreparingNonCategoryContainer()
    {
      new CategoryItemsLayoutMock().CallPrepareContainerForItemOverride(new DependencyObject(), "item");
    }
  }
}
