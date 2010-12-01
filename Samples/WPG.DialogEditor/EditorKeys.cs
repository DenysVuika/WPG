using System.Windows;

namespace DialogEditor
{
  public static class EditorKeys
  {
    private static ComponentResourceKey _FilePathPickerEditorKey =
      new ComponentResourceKey(typeof(EditorKeys), "FilePathPickerEditor");

    public static ComponentResourceKey FilePathPickerEditorKey
    {
      get { return _FilePathPickerEditorKey; }
    }
  }
}
