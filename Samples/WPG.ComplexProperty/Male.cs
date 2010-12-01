using System.ComponentModel;
namespace ComplexProperty
{
  public class Male : Human
  {
    private Female _wife = new Female { Name = "Joan", Surname = "Doe" };
        
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Female Wife
    {
      get { return _wife; }
      set
      {
        if (_wife == value) return;
        _wife = value;
        OnPropertyChanged("Wife");
      }
    }
  }
}
