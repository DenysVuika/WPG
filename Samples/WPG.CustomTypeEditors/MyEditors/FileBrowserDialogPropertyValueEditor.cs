using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.Win32;

namespace Sample4.CustomTypeEditors
{
  // TODO: Refactoring needed!
  public class FileBrowserDialogPropertyValueEditor : Editor
  {
    public FileBrowserDialogPropertyValueEditor()
    {
      InlineTemplate = LocalResources.FileBrowserEditorKey;
    }

    public override void ShowDialog(PropertyItemValue propertyValue, IInputElement commandSource)
    {
      if (propertyValue == null) return;
      if (propertyValue.ParentProperty.IsReadOnly) return;

      var ofd = new OpenFileDialog();
      ofd.Multiselect = false;

      var property = propertyValue.ParentProperty;
      if (property != null)
      {
        var optionsAttribute = (OpenFileDialogOptionsAttribute)property.Attributes[typeof(OpenFileDialogOptionsAttribute)];
        if (optionsAttribute != null)
          optionsAttribute.ConfigureDialog(ofd);
      }

      if (ofd.ShowDialog() == true)
        propertyValue.StringValue = ofd.FileName;
    }
  }
}
