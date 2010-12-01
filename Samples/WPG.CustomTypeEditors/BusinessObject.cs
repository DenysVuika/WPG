using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.WpfPropertyGrid;
using System.Windows.Media;
using System.Windows.Markup;

namespace Sample4.CustomTypeEditors
{
  public enum VideoDevices
  {
    UNSPECIFIED,
    [Description("Microsoft VX 6000")]
    MICROSOFT_VX_6000,
    [Description("Logitech Pro 5000")]
    LOGITECH_PRO_5000
  };

  public class ComplexProperty : INotifyPropertyChanged
  {
    private string _Name;
    private int _Age;

    [NotifyParentProperty(true)]
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

    public int Age
    {
      get { return _Age; }
      set
      {
        if (_Age == value) return;
        _Age = value;
        OnPropertyChanged("Age");
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
      return (string.IsNullOrEmpty(_Name))
        ? "(noname)"
        : _Name;
    }
  }

  //===============================================================
  // Uncomment the attributes below to show the Name property only
  //===============================================================
  //[BrowsableProperty(BrowsablePropertyAttribute.All, false)]
  //[BrowsableProperty("Name", true)]

  //===============================================================
  // Uncomment the attributes below to show "Display" category only
  //===============================================================
  //[BrowsableCategory(BrowsableCategoryAttribute.All, false)]
  //[BrowsableCategory("Display", true)]      
  public class BusinessObject : DependencyObject, INotifyPropertyChanged
  {
    #region Dependency property metadata definition

    public static readonly DependencyProperty RegisteredDateProperty =
      DependencyProperty.Register("RegisteredDate", typeof(DateTime), typeof(BusinessObject), new PropertyMetadata(DateTime.Now));

    public static readonly DependencyProperty NameProperty =
      DependencyProperty.Register("Name", typeof(string), typeof(BusinessObject), new PropertyMetadata("Noname"));

    public static readonly DependencyProperty PasswordProperty =
      DependencyProperty.Register("Password", typeof(string), typeof(BusinessObject));

    public static readonly DependencyProperty AttachmentProperty =
      DependencyProperty.Register("Attachment", typeof(string), typeof(BusinessObject));

    public static readonly DependencyProperty RangeProperty =
      DependencyProperty.Register("Range", typeof(double), typeof(BusinessObject), new PropertyMetadata(0.0));

    public static readonly DependencyProperty BorderThicknessProperty =
      Control.BorderThicknessProperty.AddOwner(typeof(BusinessObject), new PropertyMetadata(new Thickness(1)));

    public static readonly DependencyProperty RenderTransformOriginProperty =
      Control.RenderTransformOriginProperty.AddOwner(typeof(BusinessObject), new PropertyMetadata(new Point(0, 0)));

    public static readonly DependencyProperty WidthProperty =
      Control.WidthProperty.AddOwner(typeof(BusinessObject), new PropertyMetadata(20.0));
            
    public static readonly DependencyProperty DockProperty =
      DependencyProperty.Register("Dock", typeof(Dock), typeof(BusinessObject));
    #endregion

    #region Default FontFamily Editor

    private FontFamily _FontFamilyPlain = new FontFamily("Andalus");

    public FontFamily FontFamilyPlain
    {
      get { return _FontFamilyPlain; }
      set
      {
        if (_FontFamilyPlain == value) return;
        _FontFamilyPlain = value;
        OnPropertyChanged("FontFamilyPlain");
      }
    }

    public static readonly DependencyProperty DefaultFontFamilyProperty = 
      DependencyProperty.Register("DefaultFontFamily", typeof(FontFamily), typeof(BusinessObject),
      new FrameworkPropertyMetadata(null, OnDefaultFontFamilyPropertyChanged));

    private static void OnDefaultFontFamilyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
    }

    public FontFamily DefaultFontFamily
    {
      get { return (FontFamily)GetValue(DefaultFontFamilyProperty); }
      set { SetValue(DefaultFontFamilyProperty, value); }
    }

      //Control.FontFamilyProperty.AddOwner(typeof(BusinessObject),
      //new FrameworkPropertyMetadata(Control.FontFamilyProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));

    #endregion

    private ComplexProperty _ComplexProperty = new ComplexProperty();

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ComplexProperty ComplexProperty
    {
      get { return _ComplexProperty; }
      set
      {
        if (_ComplexProperty == value) return;
        _ComplexProperty = value;
        OnPropertyChanged("ComplexValue");
      }
    }

    [Category("Account information")]
    [DisplayName("Login")]
    [Description("This property supports both Inline and Extended value editors")]    
    [Browsable(false)]
    public string Name
    {
      get { return (string)GetValue(NameProperty); }
      set { SetValue(NameProperty, value); }
    }

    [Category("Account information")]
    [Description("Type your password here")]    
    [MergableProperty(false)]
    [Browsable(false)]
    public string Password
    {
      get { return (string)GetValue(PasswordProperty); }
      set { SetValue(PasswordProperty, value); }
    }

    [Category("Account information")]
    [ParenthesizePropertyName(true)]
    [Description("This property is marked with ParenthesizePropertyName attribute...")]
    [Browsable(false)]
    public DateTime RegisteredDate
    {
      get { return (DateTime)GetValue(RegisteredDateProperty); }
      set { SetValue(RegisteredDateProperty, value); }
    }
        
    [Description("Choose file to be attached.")]
    [PropertyEditor(typeof(FileBrowserDialogPropertyValueEditor))]
    [OpenFileDialogOptions("Text Files (*.txt)|*.txt", Title="Select a text file")]
    public string Attachment
    {
      get { return (string)GetValue(AttachmentProperty); }
      set { SetValue(AttachmentProperty, value); }
    }

    [Description("Choose value between 0 and 50 here.")]
    [NumberRangeAttribute(0, 50, 1)]    
    public double Range
    {
      get { return (double)GetValue(RangeProperty); }
      set { SetValue(RangeProperty, value); }
    }

    [Category("Display")]
    public Thickness BorderThickness
    {
      get { return (Thickness)GetValue(BorderThicknessProperty); }
      set { SetValue(BorderThicknessProperty, value); }
    }

    [Category("Display")]
    public Point RenderTransformOrigin
    {
      get { return (Point)GetValue(RenderTransformOriginProperty); }
      set { SetValue(RenderTransformOriginProperty, value); }
    }

    [Category("Display")]
    public double Width
    {
      get { return (double)GetValue(WidthProperty); }
      set { SetValue(WidthProperty, value); }
    }
        
    [Category("Display")]
    public Dock Dock
    {
      get { return (Dock)GetValue(DockProperty); }
      set { SetValue(DockProperty, value); }
    }

    const string CustomCategory = "Custom Int32 editor with PropertyOrder";

    private int _Integer1;
    [Category(CustomCategory)]
    [PropertyOrder(3)]
    public int Integer1
    {
      get { return _Integer1; }
      set
      {
        if (_Integer1 == value) return;
        _Integer1 = value;
        OnPropertyChanged("Integer1");
      }
    }

    [Category(CustomCategory)]
    [PropertyOrder(2)]
    public int Integer2 { get; set; }
        
    [Category(CustomCategory)]
    [PropertyOrder(1)]
    public int Integer3 { get; set; }

    [Category(CustomCategory)]
    [PropertyOrder(0)]
    public int Integer4 { get; set; }

    private static BusinessObject _currentObject;

    public BusinessObject()
    {
      _currentObject = this;
    }

    /** Example For validation Callback **/
    public static readonly DependencyProperty CameraProperty =
      DependencyProperty.Register("Camera",
        typeof(VideoDevices),
        typeof(BusinessObject),
        new PropertyMetadata(VideoDevices.UNSPECIFIED), validateDevice);

    [Description("Camera for validate Example")]
    public VideoDevices Camera
    {
      get { return (VideoDevices)GetValue(CameraProperty); }
      set { SetValue(CameraProperty, value); }
    }

    private static bool isDeviceLegal(VideoDevices device)
    {
      return device != VideoDevices.LOGITECH_PRO_5000;
    }

    //Validate Callback
    private static bool validateDevice(object value)
    {
      //make sure the video device is not used by another source.
      if (_currentObject == null)
        return true;
      if (!isDeviceLegal((VideoDevices)value))
      {
        MessageBox.Show("Device is Not Legal", "Device Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //This should throw an Error - but we expect the value in the combo Box
        //to show the previous item.
        return false;
      }

      return true;
    }

    private double _doubleBlend;

    [Description("Drag the value with the mouse or click the middle of the control to enable keyboard")]
    [Category("Double editors")]
    [DisplayName("Double (Blend)")]
    public double DoubleBlend 
    {
      get { return _doubleBlend; }
      set
      {
        if (_doubleBlend == value) return;
        _doubleBlend = value;
        OnPropertyChanged("DoubleBlend");
      }
    }

    private double _doubleCommon;

    [Category("Double editors")]
    [Description("Default (Texbox-based) editor is used for this property")]
    [DisplayName("Double (Common)")]
    public double DoubleCommon
    {
      get { return _doubleCommon; }
      set
      {
        if (_doubleCommon == value) return;
        _doubleCommon = value;
        OnPropertyChanged("DoubleCommon");
      }
    }

    private static readonly DependencyProperty TimeSpanProperty =
      DependencyProperty.Register("TimeSpan", typeof(TimeSpan), typeof(BusinessObject), new PropertyMetadata(new TimeSpan(10, 20, 30)));
        
    #region Customizing "Text" category

    public static readonly DependencyProperty FontFamilyProperty = Control.FontFamilyProperty.AddOwner(typeof(BusinessObject), 
      new FrameworkPropertyMetadata(Control.FontFamilyProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));

    public static readonly DependencyProperty FontSizeProperty = Control.FontSizeProperty.AddOwner(typeof(BusinessObject),
      new FrameworkPropertyMetadata(Control.FontSizeProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));

    public static readonly DependencyProperty FontWeightProperty = Control.FontWeightProperty.AddOwner(typeof(BusinessObject),
      new FrameworkPropertyMetadata(Control.FontWeightProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));

    public static readonly DependencyProperty FontStyleProperty = Control.FontStyleProperty.AddOwner(typeof(BusinessObject),
      new FrameworkPropertyMetadata(Control.FontStyleProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));
    
    [Category("Text")]
    public FontFamily FontFamily
    {
      get { return (FontFamily)GetValue(FontFamilyProperty); }
      set { SetValue(FontFamilyProperty, value); }
    }

    [Category("Text")]
    public double FontSize
    {
      get { return (double)GetValue(FontSizeProperty); }
      set { SetValue(FontSizeProperty, value); }
    }

    [Category("Text")]
    public FontWeight FontWeight
    {
      get { return (FontWeight)GetValue(FontWeightProperty); }
      set { SetValue(FontWeightProperty, value); }
    }

    [Category("Text")]
    public FontStyle FontStyle
    {
      get { return (FontStyle)GetValue(FontStyleProperty); }
      set { SetValue(FontStyleProperty, value); }
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    private string _percentage;
    public string Percentage
    {
      get
      {
        return _percentage;
      }
      set
      {
        if (_percentage == value) return;
        _percentage = value;
        OnPropertyChanged("Percentage");
      }
    }

    public XmlLanguage Language { get; set; }
    public Brush BackgroundBrush { get; set; }
  }
}