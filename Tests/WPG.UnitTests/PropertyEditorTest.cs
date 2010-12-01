using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class PropertyEditorTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExpectsDeclaringTypeProvided()
    {
      new PropertyEditor(null, "name");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExpectsPropertyNameProvided()
    {
      new PropertyEditor(typeof(string), null);
    }

    [TestMethod]
    public void AssignsDeclaringTypeProperty()
    {
      PropertyEditor editor = new PropertyEditor(typeof(string), "name");
      Assert.AreEqual<Type>(typeof(string), editor.DeclaringType);

      editor = new PropertyEditor { DeclaringType = typeof(int) };
      Assert.AreEqual<Type>(typeof(int), editor.DeclaringType);
    }

    [TestMethod]
    public void AssignsPropertyNameProperty()
    {
      PropertyEditor editor = new PropertyEditor(typeof(string), "name");
      Assert.AreEqual<string>("name", editor.PropertyName);

      editor = new PropertyEditor { PropertyName = "name" };
      Assert.AreEqual<string>("name", editor.PropertyName);
    }

    [TestMethod]
    public void AssignsInlineTemplate()
    {
      DataTemplate template = new DataTemplate();
      Editor editor = new PropertyEditor(typeof(string), "name", template);
      Assert.AreEqual(template, editor.InlineTemplate);
    }
  }
}
