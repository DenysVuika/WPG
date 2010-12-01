using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace WpfPropertyGrid.UnitTests.Design
{
  [TestClass]
  public class GridEntryContainerTest
  {
    #region Mock Objects
    private class GridEntryContainerMock : GridEntryContainer
    {
      public GridEntryContainerMock() { }
      
      public GridEntryContainerMock(ResourceLocator resourceLocator)
      {
        this.ResourceLocator = resourceLocator;
      }
    }

    private class GridEntryMock : GridEntry
    {
      public override void ApplyFilter(PropertyFilter filter)
      {
      }

      public override bool MatchesPredicate(PropertyFilterPredicate predicate)
      {
        return false;
      }
    }

    private class EditorMock : Editor
    {      
    }

    private class ResourceLocatorMock : ResourceLocator
    {
      private object _key, _result;
      
      public ResourceLocatorMock(object key, object result)
      {
        _key = key;
        _result = result;
      }

      public override object GetResource(object key)
      {
        if (key == _key) return _result;
        return null;
      }
    }
    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailGettingParentContainerForNull()
    {
      GridEntryContainer.GetParentContainer(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailSettingParentContainerForNull()
    {
      GridEntryContainer.SetParentContainer(null, new GridEntryContainerMock());
    }

    [TestMethod]
    public void ShouldResetParentContainerWithNull()
    {
      GridEntryContainerMock container = new GridEntryContainerMock();
      GridEntryContainer.SetParentContainer(container, container);
      Assert.AreEqual(container, GridEntryContainer.GetParentContainer(container));

      GridEntryContainer.SetParentContainer(container, null);
      Assert.IsNull(GridEntryContainer.GetParentContainer(container));
    }

    [TestMethod]
    public void ShoudNotReturnTemplateForMissingEntry()
    {
      Assert.IsNull(new GridEntryContainerMock().EditorTemplate);
    }

    [TestMethod]
    public void ShouldNotReturnTemplateForEntryWithoutEditor()
    {
      GridEntryContainerMock container = new GridEntryContainerMock { Entry = new GridEntryMock() };
      Assert.IsNull(container.EditorTemplate);
    }

    [TestMethod]
    public void ShouldReturnEditorWithDataTemplate()
    {
      DataTemplate template = new DataTemplate();
      GridEntryMock entry = new GridEntryMock { Editor = new EditorMock { InlineTemplate = template } };
      GridEntryContainerMock container = new GridEntryContainerMock { Entry = entry };

      Assert.AreEqual<DataTemplate>(template, container.EditorTemplate);
    }

    [TestMethod]
    public void ShouldReturnEditorWithComponentResourceKey()
    {
      ComponentResourceKey key = new ComponentResourceKey();
      DataTemplate template = new DataTemplate();

      ResourceLocatorMock resourceLocator = new ResourceLocatorMock(key, template);

      GridEntryMock entry = new GridEntryMock { Editor = new EditorMock { InlineTemplate = key } };
      GridEntryContainerMock container = new GridEntryContainerMock(resourceLocator) { Entry = entry };

      Assert.AreEqual(template, container.EditorTemplate);      
    }

    [TestMethod]
    public void ShouldReturnNothingForWrongComponentKey()
    {
      ComponentResourceKey key = new ComponentResourceKey();
      ResourceLocatorMock resourceLocator = new ResourceLocatorMock(key, null);

      GridEntryMock entry = new GridEntryMock { Editor = new EditorMock { InlineTemplate = key } };
      GridEntryContainerMock container = new GridEntryContainerMock(resourceLocator) { Entry = entry };

      Assert.IsNull(container.EditorTemplate);
    }
  }
}
