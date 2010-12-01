using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CategoryEditor
{
  public class FontWeightConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var fw = (FontWeight)value;
      return fw == FontWeights.Bold;
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
