using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;

namespace Sample4.CustomTypeEditors
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1
  {
      readonly BusinessObject bo;

    public Window1()
    {
      InitializeComponent();

      bo = new BusinessObject();

      propertyGrid.SelectedObjectsChanged += propertyGrid_SelectedObjectsChanged;
      propertyGrid.SelectedObject = bo;
      propertyGrid.PropertyValueChanged += propertyGrid_PropertyValueChanged;
    }

    void propertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
      
    }

    void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
    {
      
    }

    private void btnTest_Click(object sender, RoutedEventArgs e)
    {
      PropertyItem prop = propertyGrid.Properties["Name"];
      if (prop != null)
      {
        //prop.IsReadOnly = !prop.IsReadOnly;
        prop.IsBrowsable = !prop.IsBrowsable;
      }
    }

    static Random random = new Random();

    private void btnSwitchObjects_Click(object sender, RoutedEventArgs e)
    {
      propertyGrid.SelectedObject = new BusinessObject
      {
        Name = Path.GetRandomFileName(),
        Password = Path.GetRandomFileName(),
        RegisteredDate = DateTime.Now,
        Integer1 = random.Next(1000),
        Integer2 = random.Next(1000),
        Integer3 = random.Next(1000),
        Integer4 = random.Next(1000),
        Attachment = Path.GetRandomFileName()
      };

      //propertyGrid.Properties["Name"].SetPropertySouce(new BusinessObject { Name = Path.GetRandomFileName() });
    }

    private void btnSelectMultiple_Click(object sender, RoutedEventArgs e)
    {
      object[] objects = new object[]
      {
        new BusinessObject { Name = "Denis Vuyka", Integer1 = 10 },
        new BusinessObject { Name = "Marilyn Manson", Integer1 = 10 },
        new BusinessObject { Name = "Charles Darvin", Integer1 = 10 }
      };

      this.propertyGrid.SelectedObjects = objects;
    }
  }
}
