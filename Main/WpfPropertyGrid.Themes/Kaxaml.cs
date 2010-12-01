using System.Windows.Markup;

namespace System.Windows.Controls.WpfPropertyGrid.Themes
{
  public class KaxamlTheme : ResourceDictionary, IComponentConnector
  {
    private bool isInitialized;

    public static ComponentResourceKey StyleKey
    {
      get { return new ComponentResourceKey(typeof(KaxamlTheme), "Style"); }
    }

    public KaxamlTheme()
    {
      InitializeComponent();
    }

    #region IComponentConnector Members

    public void Connect(int connectionId, object target)
    {
      this.isInitialized = true;
    }

    public void InitializeComponent()
    {
      if (!isInitialized)
      {
        this.isInitialized = true;
        Uri resourceLocator = new Uri("/System.Windows.Controls.WpfPropertyGrid.Themes;component/Kaxaml/Theme.xaml", UriKind.Relative);
        Application.LoadComponent(this, resourceLocator);
      }
    }

    #endregion
  }
}
