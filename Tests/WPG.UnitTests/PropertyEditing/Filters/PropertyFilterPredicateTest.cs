using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WpfPropertyGrid.UnitTests.PropertyEditing.Filters
{
  [TestClass]
  public class PropertyFilterPredicateTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyMatchText()
    {
      new PropertyFilterPredicate(null);
    }

    [TestMethod]
    public void ShouldInitializeMatchTextUpperCased()
    {
      PropertyFilterPredicate predicate = new PropertyFilterPredicate("test");
      Assert.AreEqual<string>("TEST", predicate.MatchText);
    }

    [TestMethod]
    public void ShouldNotMatchNullText()
    {
      Assert.IsFalse(new PropertyFilterPredicate("test").Match(null));
    }

    [TestMethod]
    public void ShouldMatchText()
    {
      PropertyFilterPredicate predicate = new PropertyFilterPredicate("test");
      Assert.IsTrue(predicate.Match("Test"));
      Assert.IsTrue(predicate.Match("TestMe"));
      Assert.IsTrue(predicate.Match("ThisIsATest"));

    }
  }
}
