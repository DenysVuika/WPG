using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sample4.CustomTypeEditors
{
  public class FontStyleConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      FontStyle fs = (FontStyle)value;
      return fs == FontStyles.Italic;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value != null)
      {
        bool isSet = (bool)value;

        if (isSet)
        {
          return FontStyles.Italic;
        }
      }

      return FontStyles.Normal;
    }
  }
}
