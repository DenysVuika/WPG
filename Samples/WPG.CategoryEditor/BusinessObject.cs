using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace CategoryEditor
{
  /// <summary>
  /// Sample business object
  /// </summary>
  public class BusinessObject : INotifyPropertyChanged
  {
    #region Fields

    private string _name;
    private FontFamily _fontFamily = new FontFamily("Arial");
    private double _fontSize = 8;
    private FontWeight _fontWeight = FontWeights.Bold;
    private FontStyle _fontStyle = FontStyles.Italic;

    #endregion



    public string Name
    {
      get { return _name; }
      set
      {
        if (_name == value) return;
        _name = value;
        OnPropertyChanged("Name");
      }
    }

    #region Text-related properties

    [Category("Text")]
    public FontFamily FontFamily
    {
      get { return _fontFamily; }
      set
      {
        if (_fontFamily == value) return;
        _fontFamily = value;
        OnPropertyChanged("FontFamily");
      }
    }

    [Category("Text")]
    public double FontSize
    {
      get { return _fontSize; }
      set
      {
        if (_fontSize == value) return;
        _fontSize = value;
        OnPropertyChanged("FontSize");
      }
    }

    [Category("Text")]
    public FontWeight FontWeight
    {
      get { return _fontWeight; }
      set
      {
        if (_fontWeight == value) return;
        _fontWeight = value;
        OnPropertyChanged("FontWeight");
      }
    }

    [Category("Text")]
    public FontStyle FontStyle
    {
      get { return _fontStyle; }
      set
      {
        if (_fontStyle == value) return;
        _fontStyle = value;
        OnPropertyChanged("FontStyle");
      }
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
