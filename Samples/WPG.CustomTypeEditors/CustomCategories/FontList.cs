using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Sample4.CustomTypeEditors
{
  public class FontList : ObservableCollection<FontFamily>
  {
    public FontList()
    {
      foreach (var ff in Fonts.SystemFontFamilies)
      {
        Add(ff);
      }
    }
  }
}
