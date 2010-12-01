using System.Windows.Controls;
using System.Windows.Controls.WpfPropertyGrid.Design;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class CategoryEditorContentPresenterTest
  {
    [TestMethod]
    public void ShouldAssignContentBindingByDefault()
    {
      CategoryEditorContentPresenter presenter = new CategoryEditorContentPresenter();
      BindingExpression expression = presenter.GetBindingExpression(ContentPresenter.ContentProperty);
      Assert.IsNotNull(expression);
    }

    [TestMethod]
    public void ShouldAssignContentTemplateBindingByDefault()
    {
      CategoryEditorContentPresenter presenter = new CategoryEditorContentPresenter();
      BindingExpression expression = presenter.GetBindingExpression(ContentPresenter.ContentTemplateProperty);
      Assert.IsNotNull(expression);
    }
  }
}
