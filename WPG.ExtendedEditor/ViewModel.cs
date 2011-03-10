using System.ComponentModel;

namespace WPG.ExtendedEditor
{
  public class ViewModel : INotifyPropertyChanged
  {
    private string _firstName;
    private string _lastName;
    private string _details;

    public string FirstName
    {
      get { return _firstName; }
      set
      {
        if (_firstName == value) return;
        _firstName = value;
        OnPropertyChanged("FirstName");
      }
    }

    public string LastName
    {
      get { return _lastName; }
      set
      {
        if (_lastName == value) return;
        _lastName = value;
        OnPropertyChanged("LastName");
      }
    }

    public string Details
    {
      get { return _details; }
      set
      {
        if (_details == value) return;
        _details = value;
        OnPropertyChanged("Details");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
