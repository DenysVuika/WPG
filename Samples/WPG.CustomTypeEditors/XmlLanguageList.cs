using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Markup;

namespace Sample4.CustomTypeEditors
{
  public sealed class XmlLanguageList : ObservableCollection<XmlLanguage>
  {
    public XmlLanguageList()
    {
      foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        if (string.IsNullOrEmpty(ci.IetfLanguageTag)) continue;
        Add(XmlLanguage.GetLanguage(ci.IetfLanguageTag));
      }
    }
  }
}
