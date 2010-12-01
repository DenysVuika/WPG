using System;
using Microsoft.Win32;

namespace Sample4.CustomTypeEditors
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class OpenFileDialogOptionsAttribute : Attribute
  {
    public string Filter { get; protected set; }
    public bool AddExtension { get; set; }
    public bool CheckFileExists { get; set; }
    public bool CheckPathExists { get; set; }
    public string DefaultExt { get; set; }
    public bool DereferenceLinks { get; set; }
    public int FilterIndex { get; set; }
    public bool Multiselect { get; set; }
    public bool ReadOnlyChecked { get; set; }
    public string Title { get; set; }
    public bool ShowReadOnly { get; set; }
    public bool ValidateNames { get; set; }

    public OpenFileDialogOptionsAttribute(string filter)
    {
      if (string.IsNullOrEmpty(filter)) throw new ArgumentNullException("filter");
      ReadConfiguration(null);
      Filter = filter;
    }

    public bool HasTitle
    {
      get { return !string.IsNullOrEmpty(Title); }
    }

    public bool HasDefaultExt
    {
      get { return !string.IsNullOrEmpty(DefaultExt); }
    }

    public virtual void ReadConfiguration(OpenFileDialog dialog)
    {
      if (dialog == null) dialog = new OpenFileDialog();
      AddExtension = dialog.AddExtension;
      CheckFileExists = dialog.CheckFileExists;
      CheckPathExists = dialog.CheckPathExists;
      DefaultExt = dialog.DefaultExt;
      DereferenceLinks = dialog.DereferenceLinks;
      Filter = dialog.Filter;
      FilterIndex = dialog.FilterIndex;
      Multiselect = dialog.Multiselect;
      ReadOnlyChecked = dialog.ReadOnlyChecked;
      ShowReadOnly = dialog.ShowReadOnly;
      Title = dialog.Title;
      ValidateNames = dialog.ValidateNames;
    }

    public virtual void ConfigureDialog(OpenFileDialog dialog)
    {
      if (dialog == null) dialog = new OpenFileDialog();

      dialog.Filter = Filter;
      dialog.AddExtension = AddExtension;
      dialog.CheckFileExists = CheckFileExists;
      dialog.CheckPathExists = CheckPathExists;
      if (HasDefaultExt) dialog.DefaultExt = DefaultExt;
      dialog.DereferenceLinks = DereferenceLinks;
      dialog.FilterIndex = FilterIndex;
      dialog.Multiselect = Multiselect;
      dialog.ReadOnlyChecked = ReadOnlyChecked;
      dialog.ShowReadOnly = ShowReadOnly;
      if (HasTitle) dialog.Title = Title;
      dialog.ValidateNames = ValidateNames;
    }
  }
}
