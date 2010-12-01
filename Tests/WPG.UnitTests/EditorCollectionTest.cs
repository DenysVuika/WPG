using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.ComponentModel;

namespace WpfPropertyGrid.UnitTests
{
  [TestClass]
  public class EditorCollectionTest
  {
    #region Mock Objects

    private class PropertyEditorMock : PropertyEditor
    {
    }

    private class InvalidPropertyEditorMock : PropertyEditor
    {
      public InvalidPropertyEditorMock(object dummy) { }
    }

    private class CategoryEditorMock : CategoryEditor
    {
    }

    private class InvalidCategoryEditorMock : CategoryEditor
    {
      public InvalidCategoryEditorMock(object dummy) { }
    }

    [CategoryEditor("category", typeof(CategoryEditorMock))]
    [CategoryEditor("invalid1", typeof(int))]
    [CategoryEditor("invalid2", typeof(InvalidCategoryEditorMock))]
    [CategoryEditor("invalid3", "invalid")]
    private class BusinessObject
    {
    }

    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingTypeEditorForNullType()
    {
      new EditorCollection().FindTypeEditor(null);
    }

    [TestMethod]
    public void ShouldNotFindTypeEditorByTypeName()
    {
      Assert.IsNull(new EditorCollection().FindTypeEditor(typeof(int)));
    }

    [TestMethod]
    public void ShouldFindTypeEditorByTypeName()
    {
      EditorCollection editors = new EditorCollection();      
      TypeEditor editor = new TypeEditor(typeof(int), new DataTemplate());
      editors.Add(editor);

      Assert.AreEqual<TypeEditor>(editor, editors.FindTypeEditor(typeof(int)));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailFindingPropertyEditorWithNullType()
    {
      new EditorCollection().FindPropertyEditor(null, "property");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailFindingPropertyEditorWithNullName()
    {
      new EditorCollection().FindPropertyEditor(typeof(int), null);
    }

    [TestMethod]
    public void ShouldFindPropertyEditor()
    {
      var editors = new EditorCollection();
      var editor = new PropertyEditor(typeof(int), "property");
      editors.Add(editor);

      Assert.AreEqual<PropertyEditor>(editor, editors.FindPropertyEditor(typeof(int), "property"));
    }

    [TestMethod]
    public void ShouldNotFindPropertyEditor()
    {
      Assert.IsNull(new EditorCollection().FindPropertyEditor(typeof(int), "property"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailFindingCategoryEditorWithNullType()
    {
      new EditorCollection().FindCategoryEditor(null, "category");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailFindingCategoryEditorWithNullName()
    {
      new EditorCollection().FindCategoryEditor(typeof(int), null);
    }

    [TestMethod]
    public void ShouldFindCategoryEditor()
    {
      EditorCollection editors = new EditorCollection();
      CategoryEditor editor = new CategoryEditor(typeof(int), "category", new DataTemplate());
      editors.Add(editor);

      Assert.AreEqual<CategoryEditor>(editor, editors.FindCategoryEditor(typeof(int), "category"));
    }

    [TestMethod]
    public void ShouldNotFindCategoryEditor()
    {
      Assert.IsNull(new EditorCollection().FindCategoryEditor(typeof(int), "category"));
    }

    [TestMethod]
    public void ShouldReturnNoPropertyEditorByAttributes()
    {
      Assert.IsNull(EditorCollection.GetPropertyEditorByAttributes(null));
      Assert.IsNull(EditorCollection.GetPropertyEditorByAttributes(new AttributeCollection()));

      PropertyEditorAttribute attribute = new PropertyEditorAttribute(typeof(string));
      Editor editor = EditorCollection.GetPropertyEditorByAttributes(new AttributeCollection(new Attribute[] { attribute }));
      Assert.IsNull(editor);

      attribute = new PropertyEditorAttribute("missing");
      editor = EditorCollection.GetPropertyEditorByAttributes(new AttributeCollection(new Attribute[] { attribute }));
      Assert.IsNull(editor);

      attribute = new PropertyEditorAttribute(typeof(InvalidPropertyEditorMock));
      editor = EditorCollection.GetPropertyEditorByAttributes(new AttributeCollection(new Attribute[] { attribute }));
      Assert.IsNull(editor);
    }

    [TestMethod]
    public void ShouldReturnPropertyEditorByAttributes()
    {
      PropertyEditorAttribute attribute = new PropertyEditorAttribute(typeof(PropertyEditorMock));
      Editor editor = EditorCollection.GetPropertyEditorByAttributes(new AttributeCollection(new Attribute[] { attribute }));
      
      Assert.IsNotNull(editor);
      Assert.IsInstanceOfType(editor, typeof(PropertyEditorMock));
    }

    [TestMethod]
    public void ShouldGetCategoryEditor()
    {
      Editor editor = EditorCollection.GetCategoryEditorByAttributes(typeof(BusinessObject), "category");
      Assert.IsNotNull(editor);
      Assert.IsInstanceOfType(editor, typeof(CategoryEditorMock));
    }

    [TestMethod]
    public void ShouldGetNoCategoryEditor()
    {
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(null, "category"));
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(typeof(int), null));
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(typeof(BusinessObject), "missing"));
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(typeof(BusinessObject), "invalid1"));
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(typeof(BusinessObject), "invalid2"));
      Assert.IsNull(EditorCollection.GetCategoryEditorByAttributes(typeof(BusinessObject), "invalid3"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingEditorForNullCategoryItem()
    {
      new EditorCollection().GetEditor((CategoryItem)null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingEditorForNullPropertyItem()
    {
      new EditorCollection().GetEditor((PropertyItem)null);
    }
  }
}
