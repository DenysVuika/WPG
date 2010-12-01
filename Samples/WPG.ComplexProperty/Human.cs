using System.ComponentModel;

namespace ComplexProperty
{ 
  public class Human : INotifyPropertyChanged
  {
    private string _name = "John";
    private string _surname = "Doe";
    
    [NotifyParentProperty(true)]
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

    public string Surname
    {
      get { return _surname; }
      set
      {
        if (_surname == value) return;
        _surname = value;
        OnPropertyChanged("Surname");
      }
    }
        
    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    public override string ToString()
    {
      return (string.IsNullOrEmpty(_name))
        ? "(unknown)"
        : _name;
    }
  }
}
