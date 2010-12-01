using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class PropertyGridUtilsTest
  {
    [BrowsableCategory("category1", false)]
    [BrowsableCategory("category2", true)]
    private class PropertyGridUtilsTestCase
    {
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullObject()
    {
      PropertyGridUtils.GetAttributes<BrowsableCategoryAttribute>((object)null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullType()
    {
      PropertyGridUtils.GetAttributes<BrowsableCategoryAttribute>((Type)null);
    }

    [TestMethod]
    [Ignore] // TODO: This test is not reliable as Reflection does not always return the same order
    public void ShouldReturnAttributesForType()
    {
      var attributes = PropertyGridUtils.GetAttributes<BrowsableCategoryAttribute>(typeof(PropertyGridUtilsTestCase));

      Assert.AreEqual<int>(2, attributes.Count());
      
      Assert.AreEqual<string>("category1", attributes.First().CategoryName);
      Assert.IsFalse(attributes.First().Browsable);
      
      Assert.AreEqual<string>("category2", attributes.Last().CategoryName);
      Assert.IsTrue(attributes.Last().Browsable);
    }

    [TestMethod]
    [Ignore] // TODO: This test is not reliable as Reflection does not always return the same order
    public void ShouldReturnAttributesByInstance()
    {
      var attributes = PropertyGridUtils.GetAttributes<BrowsableCategoryAttribute>(new PropertyGridUtilsTestCase());

      Assert.AreEqual<int>(2, attributes.Count());

      Assert.AreEqual<string>("category1", attributes.First().CategoryName);
      Assert.IsFalse(attributes.First().Browsable);

      Assert.AreEqual<string>("category2", attributes.Last().CategoryName);
      Assert.IsTrue(attributes.Last().Browsable);
    }

    [TestMethod]
    public void ShoudReturnEmptyAttributeSequence()
    {
      var attributes = PropertyGridUtils.GetAttributes<BrowsablePropertyAttribute>(typeof(PropertyGridUtilsTestCase));

      Assert.IsNotNull(attributes);
      Assert.AreEqual<int>(0, attributes.Count());
    }
  }
}
