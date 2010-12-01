using System.Windows;

namespace CategoryEditor
{
  public static class EditorKeys
  {
    private static readonly ComponentResourceKey textCategoryEditorKey = 
      new ComponentResourceKey(typeof(EditorKeys), "TextCategoryEditor");

    public static ComponentResourceKey TextCategoryEditorKey
    {
      get { return textCategoryEditorKey; }
    }
  }
}
