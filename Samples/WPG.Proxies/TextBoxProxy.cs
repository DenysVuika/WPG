using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Sample6.Proxies
{
  public class TextBoxProxy : INotifyPropertyChanged, IDisposable
  {
    #region Fields

    private TextBox component;
    private DependencyPropertyDescriptor dpdWidth;
    private DependencyPropertyDescriptor dpdText;

    #endregion

    #region Properties

    public double Width
    {
      get { return (double)component.GetValue(TextBox.WidthProperty); }
      set { component.SetValue(TextBox.WidthProperty, value); }
    }

    public object Text
    {
      get { return component.GetValue(TextBox.TextProperty); }
      set { component.SetValue(TextBox.TextProperty, value); }
    }

    #endregion

    public TextBoxProxy(TextBox component)
    {
      if (component == null) throw new ArgumentNullException("component");
      this.component = component;

      CaptureComponent(this.component);
    }

    private void CaptureComponent(TextBox button)
    {
      dpdWidth = DependencyPropertyDescriptor.FromProperty(TextBox.WidthProperty, typeof(TextBox));
      dpdWidth.AddValueChanged(button, OnWidthChanged);

      dpdText = DependencyPropertyDescriptor.FromProperty(TextBox.TextProperty, typeof(TextBox));
      dpdText.AddValueChanged(button, OnTextChanged);
    }

    private void ReleaseComponent(TextBox button)
    {
      if (dpdWidth != null)
      {
        dpdWidth.RemoveValueChanged(button, OnWidthChanged);
        dpdWidth = null;
      }

      if (dpdText != null)
      {
        dpdText.RemoveValueChanged(button, OnTextChanged);
        dpdText = null;
      }
    }

    private void OnWidthChanged(object sender, EventArgs e)
    {
      OnPropertyChanged("Width");
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      OnPropertyChanged("Text");
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region IDisposable Members

    private bool disposed = false;

    ~TextBoxProxy()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (disposed) return;

      if (disposing)
      {
        ReleaseComponent(component);
      }

      disposed = true;
    }

    #endregion
  }
}
