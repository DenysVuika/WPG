using System;
using System.Collections.Generic;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.PropertyEditing.Filters
{
  [TestClass]
  public class PropertyFilterTest
  {
    private class PropertyFilterTargetTestCase : IPropertyFilterTarget
    {
      public string Name { get; private set; }

      public PropertyFilterTargetTestCase(string name)
      {
        this.Name = name;        
      }

      #region IPropertyFilterTarget Members

      public event EventHandler<PropertyFilterAppliedEventArgs> FilterApplied;

      private void OnFilterApplied()
      {
        if (FilterApplied != null) FilterApplied(this, null);
      }

      public void ApplyFilter(PropertyFilter filter)
      {
        this.MatchesFilter = (filter == null) || filter.Match(this);
      }

      public bool MatchesPredicate(PropertyFilterPredicate predicate)
      {
        if (predicate == null) return false;
        return predicate.Match(this.Name);
      }

      public bool MatchesFilter { get; protected set; }
      
      #endregion
    }

    [TestMethod]
    public void ShouldInitializeWithNullValue()
    {
      PropertyFilter filter = new PropertyFilter((IEnumerable<PropertyFilterPredicate>)null);
      Assert.IsTrue(filter.IsEmpty);
    }

    [TestMethod]
    public void ShouldInitiaizeWithPredicatesCollection()
    {
      PropertyFilter filter = new PropertyFilter(new PropertyFilterPredicate[] { new PropertyFilterPredicate("test"), new PropertyFilterPredicate("me") });
      Assert.IsFalse(filter.IsEmpty);      
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailMatchingToNullValue()
    {
      PropertyFilter filter = new PropertyFilter("test");
      filter.Match(null);
    }

    [TestMethod]
    public void ShouldPerformMatch()
    {
      PropertyFilter filter = new PropertyFilter("test");
      Assert.IsTrue(filter.Match(new PropertyFilterTargetTestCase("test")));
      Assert.IsFalse(filter.Match(new PropertyFilterTargetTestCase("missing")));
    }

    [TestMethod]
    public void ShouldReturnPositiveMatchWhenEmpty()
    {
      PropertyFilter filter = new PropertyFilter((string)null);
      Assert.IsTrue(filter.Match(new PropertyFilterTargetTestCase("test")));
    }
  }
}
