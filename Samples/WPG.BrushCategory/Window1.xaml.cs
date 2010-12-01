using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Samples.BrushCategory
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1
  {
    public Window1()
    {
      InitializeComponent();
      //the proxy will filter out the props you wish, in this case the brushes
      propertyGrid.SelectedObject = new BrushProxy( new MultiBrushObject1 {Background = Brushes.DarkCyan, BorderBrush = Brushes.Violet});
      
    }
  }



  /// <summary>
  /// This class acts as a pre-processor for properties shown in WPG.
  /// </summary>
  public class BrushProxy : DependencyObject
  {
    public object CurrentComponent { get; set; }

    #region ObjectBrushes

    /// <summary>
    /// ObjectBrushes Dependency Property
    /// </summary>
    public static readonly DependencyProperty ObjectBrushesProperty =
        DependencyProperty.Register("ObjectBrushes", typeof(BrushInfo[]), typeof(BrushProxy),
            new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnObjectBrushesChanged)));

    /// <summary>
    /// Gets or sets the ObjectBrushes property.  
    /// </summary>
    [Category("Brushes")]
    public BrushInfo[] ObjectBrushes
    {
      get { return (BrushInfo[])GetValue(ObjectBrushesProperty); }
      set { SetValue(ObjectBrushesProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ObjectBrushes property.
    /// </summary>
    private static void OnObjectBrushesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((BrushProxy)d).OnObjectBrushesChanged(e);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ObjectBrushes property.
    /// </summary>
    protected virtual void OnObjectBrushesChanged(DependencyPropertyChangedEventArgs e)
    {
      //not of interest here. Only meaningful if you alter the collection (the amount of elements).
    }

    #endregion




    public BrushProxy(object component)
    {
      if (component == null) throw new ArgumentNullException("component");
      CurrentComponent = component;

      CaptureComponent(CurrentComponent);
    }
    /// <summary>
    /// Captures the component's properties of interest.
    /// </summary>
    /// <param name="component">The component.</param>
    private void CaptureComponent(object component)
    {
      if (component == null) throw new ArgumentNullException("component");
      var props = component.GetType().GetProperties();
      //take everything inheriting from a Brush type
      //You could filter out on the basis of a name or what not.
      var brs = from p in props where typeof(Brush).IsAssignableFrom(p.PropertyType) select p;
      if (brs.Count() == 0) return;
      var list = new List<BrushInfo>();
      foreach (var info in brs)
      {
        list.Add(new BrushInfo { Name = info.Name, ObjectBrush = info.GetValue(component, null) as Brush, Info = info, Component = component });
      }
      ObjectBrushes = list.ToArray();
    }
  }
  /// <summary>
  /// Sample multi-brush object.
  /// </summary>
  public class MultiBrushObject1 : DependencyObject
  {
    #region BorderBrush

    /// <summary>
    /// BorderBrush Dependency Property
    /// </summary>
    public static readonly DependencyProperty BorderBrushProperty =
        DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(MultiBrushObject1),
            new FrameworkPropertyMetadata(Brushes.GreenYellow,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the BorderBrush property.  This dependency property 
    /// indicates ....
    /// </summary>
    [Category("Brushes")]
    public Brush BorderBrush
    {
      get { return (Brush)GetValue(BorderBrushProperty); }
      set { SetValue(BorderBrushProperty, value); }
    }

    #endregion

    #region Background

    /// <summary>
    /// Background Dependency Property
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register("Background", typeof(Brush), typeof(MultiBrushObject1),
            new FrameworkPropertyMetadata(Brushes.DarkCyan,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the Background property.  This dependency property 
    /// indicates ....
    /// </summary>
    [Category("Brushes")]
    public Brush Background
    {
      get { return (Brush)GetValue(BackgroundProperty); }
      set { SetValue(BackgroundProperty, value); }
    }

    #endregion

  }
  /// <summary>
  /// Sample multi-brush object.
  /// </summary>
  public class MultiBrushObject2 : DependencyObject
  {

    #region BorderBrush

    /// <summary>
    /// BorderBrush Dependency Property
    /// </summary>
    public static readonly DependencyProperty BorderBrushProperty =
        DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(MultiBrushObject1),
            new FrameworkPropertyMetadata(Brushes.GreenYellow,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the BorderBrush property.  This dependency property 
    /// indicates ....
    /// </summary>
    [Category("Brushes")]
    public Brush BorderBrush
    {
      get { return (Brush)GetValue(BorderBrushProperty); }
      set { SetValue(BorderBrushProperty, value); }
    }

    #endregion

    #region Background

    /// <summary>
    /// Background Dependency Property
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register("Background", typeof(Brush), typeof(MultiBrushObject1),
            new FrameworkPropertyMetadata(Brushes.DarkCyan,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the Background property.  This dependency property 
    /// indicates ....
    /// </summary>
    [Category("Brushes")]
    public Brush Background
    {
      get { return (Brush)GetValue(BackgroundProperty); }
      set { SetValue(BackgroundProperty, value); }
    }

    #endregion

    #region ShadowBrush

    /// <summary>
    /// ShadowBrush Dependency Property
    /// </summary>
    public static readonly DependencyProperty ShadowBrushProperty =
        DependencyProperty.Register("ShadowBrush", typeof(Brush), typeof(MultiBrushObject2),
            new FrameworkPropertyMetadata(Brushes.Gray,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the ShadowBrush property.  This dependency property 
    /// indicates ....
    /// </summary>
    public Brush ShadowBrush
    {
      get { return (Brush)GetValue(ShadowBrushProperty); }
      set { SetValue(ShadowBrushProperty, value); }
    }

    #endregion


  }
  /// <summary>
  /// Utility class to pass the property info to the datatemplate.
  /// The crucial method is the passing of the new values (set in WPG) via the PropertyInfo to the edited object.
  /// </summary>
  public class BrushInfo : DependencyObject
  {
    public string Name { get; set; }
    public PropertyInfo Info { get; set; }
    public object Component { get; set; }

    #region ObjectBrush

    /// <summary>
    /// ObjectBrush Dependency Property
    /// </summary>
    public static readonly DependencyProperty ObjectBrushProperty =
        DependencyProperty.Register("ObjectBrush", typeof(Brush), typeof(BrushInfo),
            new FrameworkPropertyMetadata(Brushes.Transparent,
                new PropertyChangedCallback(OnObjectBrushChanged)));

    /// <summary>
    /// Gets or sets the ObjectBrush property.  
    /// </summary>
    public Brush ObjectBrush
    {
      get { return (Brush)GetValue(ObjectBrushProperty); }
      set { SetValue(ObjectBrushProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ObjectBrush property.
    /// </summary>
    private static void OnObjectBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((BrushInfo)d).OnObjectBrushChanged(e);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ObjectBrush property.
    /// </summary>
    protected virtual void OnObjectBrushChanged(DependencyPropertyChangedEventArgs e)
    {
      if (Info != null)
      {
        Info.SetValue(Component, e.NewValue, null);
      }
    }

    #endregion


  }
  /// <summary>
  /// Sample dataset for brushes. 
  /// You should really use a custom brush editor (e.g. from Telerik) but it all depends on what your aims are, of course.
  /// </summary>
  public class BrushList : ObservableCollection<string>
  {

    public BrushList()
    {

      Add("#FFFFFFFF");
      Add("#FF000000");
      Add("#FF142233");
      Add("#FFABEF14");

    }

  }

}
