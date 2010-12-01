using System.ComponentModel;

namespace WindowsFormsIntegration
{
  public class BusinessObject : INotifyPropertyChanged
  {
    private string _Name = "Noname";
    [Category("Personal Information")]
    public string Name
    {
      get { return _Name; }
      set
      {
        if (_Name == value) return;
        _Name = value;
        OnPropertyChanged("Name");
      }
    }

    private bool _IsEnabled = true;
    [Category("Display")]
    public bool IsEnabled
    {
      get { return _IsEnabled; }
      set
      {
        if (_IsEnabled = value) return;
        _IsEnabled = value;
        OnPropertyChanged("IsEnabled");
      }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
