using System.Windows.Controls.WpfPropertyGrid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace WpfPropertyGrid.UnitTests.Metadata
{
  [TestClass]
  public class AttributesContainerTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldFailInitializingNull()
    {
      new AttributesContainer(null);
    }

    [TestMethod]
    public void ShouldInitializeWithCollection()
    {
      AttributeCollection collection = new AttributeCollection(new Attribute[] { new BrowsableAttribute(true), new ReadOnlyAttribute(true) });
      AttributesContainer container = new AttributesContainer(collection);
      Assert.IsNotNull(container["Browsable"]);
      Assert.IsNotNull(container["ReadOnly"]);
    }

    [TestMethod]
    public void ShouldReturnNullForMissingAttribute()
    {
      Assert.IsNull(new AttributesContainer(new AttributeCollection(new Attribute[0]))["Missing"]);
    }

    [TestMethod]
    public void ShouldNotRegisterEmptyName()
    {
      Assert.IsFalse(new AttributesContainer(new AttributeCollection(new Attribute[0])).RegisterAttribute(null, typeof(BrowsableAttribute)));
    }

    [TestMethod]
    public void ShouldNotRegisterEmptyType()
    {
      Assert.IsFalse(new AttributesContainer(new AttributeCollection(new Attribute[0])).RegisterAttribute("name", null));
    }

    [TestMethod]
    public void ShouldTrancateAttributeSuffix()
    {
      AttributesContainer container = new AttributesContainer(new AttributeCollection(new Attribute[0]));
      container.RegisterAttribute("ExternalAttribute", typeof(BrowsableAttribute));
      container.RegisterAttribute("Another", typeof(ReadOnlyAttribute));
      Assert.IsNotNull(container["External"]);
      Assert.IsNotNull(container["Another"]);
    }

    [TestMethod]
    public void ShouldNotRegisterAttribute()
    {
      AttributesContainer container = new AttributesContainer(new AttributeCollection(new Attribute[] { }));
      Assert.IsFalse(container.RegisterAttribute("Attribute", typeof(BrowsableAttribute)));
    }

    [TestMethod]
    public void ShouldRegisterAttributeOnlyOnce()
    {
      AttributesContainer container = new AttributesContainer(new AttributeCollection(new Attribute[0]));
      Assert.IsTrue(container.RegisterAttribute("One", typeof(BrowsableAttribute)));
      Assert.IsFalse(container.RegisterAttribute("One", typeof(BrowsableAttribute)));
    }
  }
}
