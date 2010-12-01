using System.Windows.Controls;
using System.Windows.Controls.WpfPropertyGrid.Design;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class PropertyEditorContentPresenterTest
  {
    [TestMethod]
    public void ShouldAssignContentBindingByDefault()
    {
      PropertyEditorContentPresenter presenter = new PropertyEditorContentPresenter();
      BindingExpression expression = presenter.GetBindingExpression(ContentPresenter.ContentProperty);
      Assert.IsNotNull(expression);
    }

    [TestMethod]
    public void ShouldAssignContentTemplateBindingByDefault()
    {
      PropertyEditorContentPresenter presenter = new PropertyEditorContentPresenter();
      BindingExpression expression = presenter.GetBindingExpression(ContentPresenter.ContentTemplateProperty);
      Assert.IsNotNull(expression);
    }
  }
}
