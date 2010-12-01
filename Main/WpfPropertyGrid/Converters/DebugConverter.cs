using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace System.Windows.Controls.WpfPropertyGrid
{
  /// <summary>
  /// Value converter that can be used for editor debugging purposes.   
  /// </summary>
  public class DebugConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      Debugger.Break();
      return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      Debugger.Break();
      return value;
    }

    #endregion
  }
}
