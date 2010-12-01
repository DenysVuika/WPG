using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class TypeEditorTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExpectsEditedTypeToBeDefined()
    {
      new TypeEditor(null);
    }

    [TestMethod]
    public void ShouldAssignEditedType()
    {
      TypeEditor editor = new TypeEditor(typeof(string));
      
      Assert.AreEqual<Type>(typeof(string), editor.EditedType);
    }
        
    [TestMethod]
    public void ShouldAssignInlineTemplate()
    {
      DataTemplate template = new DataTemplate();
      Editor editor = new TypeEditor(typeof(string), template);
      
      Assert.AreEqual(template, editor.InlineTemplate);      
    }

    [TestMethod]
    public void ShouldAssignInlineAndExtendedTemplates()
    {
      DataTemplate template1 = new DataTemplate();
      DataTemplate template2 = new DataTemplate();
      Editor editor = new TypeEditor(typeof(string), template1, template2);
      
      Assert.AreEqual(template1, editor.InlineTemplate);
      Assert.AreEqual(template2, editor.ExtendedTemplate);
    }

    [TestMethod]
    public void AcceptsDataTemplateForTemplates()
    {
      DataTemplate template = new DataTemplate();
      Editor editor = new TypeEditor { InlineTemplate = template, ExtendedTemplate = template, DialogTemplate = template };
      
      Assert.AreEqual(template, editor.InlineTemplate);
      Assert.AreEqual(template, editor.ExtendedTemplate);
      Assert.AreEqual(template, editor.DialogTemplate);
    }

    [TestMethod]
    public void AcceptsResourceKeyForTemplates()
    {
      ComponentResourceKey key = new ComponentResourceKey();
      Editor editor = new TypeEditor { InlineTemplate = key, ExtendedTemplate = key, DialogTemplate = key };

      Assert.AreEqual(key, editor.InlineTemplate);
      Assert.AreEqual(key, editor.ExtendedTemplate);
      Assert.AreEqual(key, editor.DialogTemplate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidatesNotSupportedTemplateValues()
    {
      Editor editor = new TypeEditor(typeof(string), "WRONG_VALUE");
    }
  }
}
