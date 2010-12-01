using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sample4.CustomTypeEditors
{
  public class FontWeightConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var fs = (FontWeight)value;
      return fs == FontWeights.Bold;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value != null)
      {
        bool isSet = (bool)value;

        if (isSet)
        {
          return FontWeights.Bold;
        }
      }

      return FontWeights.Normal;
    }
  }
}
