using System;
using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class CategoryEditorTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForNullType()
    {
      new CategoryEditor(null, "category", new DataTemplate());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyCategoryName()
    {
      new CategoryEditor(typeof(string), null, new DataTemplate());
    }

    [TestMethod]
    public void ShouldInitilizePropertiesFromConstructor()
    {
      DataTemplate template = new DataTemplate();
      CategoryEditor editor = new CategoryEditor(typeof(string), "category", template);

      Assert.AreEqual<Type>(typeof(string), editor.DeclaringType);
      Assert.AreEqual<string>("category", editor.CategoryName);
      Assert.AreEqual(template, editor.InlineTemplate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ShouldFailInitializingInvalidTemplate()
    {
      new CategoryEditor(typeof(string), "category", "invalid_value");
    }

    [TestMethod]
    public void ShouldAllowSettingCategoryName()
    {
      CategoryEditor editor = new CategoryEditor();
      editor.CategoryName = "category";

      Assert.AreEqual<string>("category", editor.CategoryName);
    }

    [TestMethod]
    public void ShouldAllowSettingDeclaringType()
    {
      CategoryEditor editor = new CategoryEditor();
      editor.DeclaringType = typeof(string);

      Assert.AreEqual<Type>(typeof(string), editor.DeclaringType);
    }   
  }
}
