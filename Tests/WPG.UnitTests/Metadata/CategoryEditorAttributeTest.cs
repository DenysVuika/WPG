using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class CategoryEditorAttributeTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyCategoryName()
    {
      new CategoryEditorAttribute(null, "TYPE");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyEditorTypeName()
    {
      new CategoryEditorAttribute("Category", string.Empty);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void ShouldFailForNullEditorType()
    {
      new CategoryEditorAttribute("Category", (Type)null);
    }

    [TestMethod]
    public void ShouldAssignCategoryName()
    {
      Assert.AreEqual<string>("CATEGORY1", new CategoryEditorAttribute("category1", "type").CategoryName);
      Assert.AreEqual<string>("CATEGORY2", new CategoryEditorAttribute("category2", typeof(string)).CategoryName);
    }

    [TestMethod]
    public void ShouldAssignCategoryEditorType()
    {
      Assert.AreEqual<string>("type1", new CategoryEditorAttribute("category", "type1").EditorType);
      Assert.AreEqual<string>(typeof(string).AssemblyQualifiedName, new CategoryEditorAttribute("category", typeof(string)).EditorType);
    }

    [TestMethod]
    public void ShouldCombineHashCodes()
    {
      int expectedHash = "CATEGORY".GetHashCode() ^ "type".GetHashCode();
      Assert.AreEqual<int>(expectedHash, new CategoryEditorAttribute("category", "type").GetHashCode());
    }

    [TestMethod]
    public void ShouldCompareSameInstance()
    {
      CategoryEditorAttribute attribute = new CategoryEditorAttribute("category", "type");
      Assert.IsTrue(attribute.Equals(attribute));
    }

    [TestMethod]
    public void ShouldCompareTwoInstances()
    {
      CategoryEditorAttribute attribute1 = new CategoryEditorAttribute("category", "type");
      CategoryEditorAttribute attribute2 = new CategoryEditorAttribute("category", "type");      
      Assert.IsTrue(attribute1.Equals(attribute2));

      attribute2 = new CategoryEditorAttribute("category2", "type");
      Assert.IsFalse(attribute1.Equals(attribute2));

      attribute2 = new CategoryEditorAttribute("category", "type2");
      Assert.IsFalse(attribute1.Equals(attribute2));
    }

    [TestMethod]
    public void ShouldCompareToNull()
    {
      Assert.IsFalse(new CategoryEditorAttribute("category", "type").Equals(null));
    }
  }
}
