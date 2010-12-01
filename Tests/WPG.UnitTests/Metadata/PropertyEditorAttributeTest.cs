using System;
using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class PropertyEditorAttributeTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailForEmptyTypeName()
    {
      new PropertyEditorAttribute(string.Empty);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void ShouldFailForEmptyType()
    {
      new PropertyEditorAttribute((Type)null);
    }

    [TestMethod]
    public void ShouldAssignTypeName()
    {
      PropertyEditorAttribute attribute = new PropertyEditorAttribute("TYPE");
      Assert.AreEqual<string>("TYPE", attribute.EditorType);
    }

    [TestMethod]
    public void ShouldAssignType()
    {
      PropertyEditorAttribute attribute = new PropertyEditorAttribute(typeof(string));
      Assert.AreEqual<string>(typeof(string).AssemblyQualifiedName, attribute.EditorType);
    }

    [TestMethod]
    public void ShouldReturnEqualtyForSameInstance()
    {
      PropertyEditorAttribute attribute = new PropertyEditorAttribute("type");
      Assert.IsTrue(attribute.Equals(attribute));
    }

    [TestMethod]
    public void ShouldReturnEqualtyByNames()
    {
      PropertyEditorAttribute attribute1 = new PropertyEditorAttribute("type");
      PropertyEditorAttribute attribute2 = new PropertyEditorAttribute("type");

      Assert.IsTrue(attribute1.Equals(attribute2));
      Assert.IsTrue(attribute2.Equals(attribute1));
    }

    [TestMethod]
    public void ShouldReturnEqualtyWithNull()
    {
      Assert.IsFalse(new PropertyEditorAttribute("type").Equals(null));
    }

    [TestMethod]
    public void ShouldReturnTypeNameBasedHashCode()
    {
      int hash1 = new PropertyEditorAttribute("type").GetHashCode();
      int hash2 = new PropertyEditorAttribute("type").GetHashCode();
      int hash3 = new PropertyEditorAttribute("newtype").GetHashCode();

      Assert.AreEqual<int>(hash1, hash2);
      Assert.AreNotEqual<int>(hash1, hash3);
    }
  }
}
