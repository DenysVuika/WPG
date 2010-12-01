using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Sample4.CustomTypeEditors.MyEditors
{
  /// <summary>
  /// Member of the Point structure.
  /// </summary>
  public enum PointDisplayMember
  {
    /// <summary>
    /// None
    /// </summary>
    None,
    /// <summary>
    /// X-Coordinate
    /// </summary>
    X,
    /// <summary>
    /// Y-Coordinate
    /// </summary>
    Y
  }

  /// <summary>
  /// An editor that provides possibilities editing members of the Point structures.
  /// </summary>
  public class PointEditor : Slider
  {
    #region Fields
    private bool _isUpdating;
    #endregion

    #region EditValue
    /// <summary>
    /// Identifies the <see cref="EditValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EditValueProperty =
      DependencyProperty.Register("EditValue", typeof(Point), typeof(PointEditor),
        new FrameworkPropertyMetadata(
          new Point(),
          FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
          OnEditValuePropertyChanged,
          OnEditValuePropertyCoerceValue,
          false,
          UpdateSourceTrigger.PropertyChanged));


    private static void OnEditValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var editor = (PointEditor)sender;
      editor.UpdateValue();
    }

    private static object OnEditValuePropertyCoerceValue(DependencyObject sender, object baseValue)
    {
      return baseValue;
    }

    /// <summary>
    /// Gets or sets the edited value. This is a dependency property.
    /// </summary>
    /// <value>The edited value.</value>
    public Point EditValue
    {
      get { return (Point)GetValue(EditValueProperty); }
      set { SetValue(EditValueProperty, value); }
    }
    #endregion

    #region DisplayMember

    /// <summary>
    /// Identifies the <see cref="DisplayMember"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisplayMemberProperty =
      DependencyProperty.Register("DisplayMember", typeof(PointDisplayMember), typeof(PointEditor), new FrameworkPropertyMetadata(PointDisplayMember.None));

    /// <summary>
    /// Gets or sets the display member. This is a dependency property.
    /// </summary>
    /// <value>The display member.</value>
    public PointDisplayMember DisplayMember
    {
      get { return (PointDisplayMember)GetValue(DisplayMemberProperty); }
      set { SetValue(DisplayMemberProperty, value); }
    }

    #endregion

    #region ctor
    /// <summary>
    /// Initializes a new instance of the <see cref="PointEditor"/> class.
    /// </summary>
    public PointEditor()
    {
      AutoToolTipPlacement = AutoToolTipPlacement.BottomRight;
      IsDirectionReversed = false;
      IsMoveToPointEnabled = true;
      IsSnapToTickEnabled = false;
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Updates the current position of the <see cref="T:System.Windows.Controls.Slider"/> when the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> property changes.
    /// </summary>
    /// <param name="oldValue">The old <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> of the <see cref="T:System.Windows.Controls.Slider"/>.</param>
    /// <param name="newValue">The new <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> of the <see cref="T:System.Windows.Controls.Slider"/>.</param>
    protected override void OnValueChanged(double oldValue, double newValue)
    {
      if (_isUpdating) return;
      base.OnValueChanged(oldValue, newValue);

      switch (DisplayMember)
      {
        case PointDisplayMember.X:
          EditValue = new Point(Math.Round(Value, AutoToolTipPrecision), EditValue.Y);
          break;
        case PointDisplayMember.Y:
          EditValue = new Point(EditValue.X, Math.Round(Value, AutoToolTipPrecision));
          break;
        default:
          break;
      }
    }
    #endregion

    #region Private members
    private void UpdateValue()
    {
      if (_isUpdating) return;

      _isUpdating = true;

      switch (DisplayMember)
      {
        case PointDisplayMember.X:
          Value = EditValue.X;
          break;
        case PointDisplayMember.Y:
          Value = EditValue.Y;
          break;
        default:
          Value = 0;
          break;
      }

      _isUpdating = false;
    }
    #endregion
  }
}
