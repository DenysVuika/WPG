using System.Windows;

namespace Sample6.Proxies
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();

      this.propertyGrid.SelectedObject = new TextBoxProxy(TargetToProxy);      
    }
  }
}
