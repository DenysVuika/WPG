using System.Windows.Forms;

namespace WindowsFormsIntegration
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      var host = new System.Windows.Forms.Integration.ElementHost { Dock = DockStyle.Fill };

      var wpg = new System.Windows.Controls.WpfPropertyGrid.PropertyGrid
      {
        Layout = new System.Windows.Controls.WpfPropertyGrid.Design.CategorizedLayout()
      };
      host.Child = wpg;

      wpg.SelectedObject = new BusinessObject();
      
      this.Controls.Add(host);
    }
  }
}
