using System.Windows.Controls.WpfPropertyGrid.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfPropertyGrid.UnitTests.Internal
{
  [TestClass]
  public class MathUtilTest
  {
    [TestMethod]
    public void ShouldValidateNumberWithinRange()
    {
      Assert.AreEqual<int>(1, MathUtil.ValidateNumber(1, 1, 1));
      Assert.AreEqual<int>(6, MathUtil.ValidateNumber(6, 5, 10));
      Assert.AreEqual<int>(5, MathUtil.ValidateNumber(4, 5, 10));
      Assert.AreEqual<int>(10, MathUtil.ValidateNumber(11, 5, 10));      
    }

    [TestMethod]
    public void ShouldValidateStringWithinRange()
    {
      Assert.AreEqual<int>(0, MathUtil.ValidateNumber("wrong", 10, 20));
      Assert.AreEqual<int>(1, MathUtil.ValidateNumber("1", 1, 1));
      Assert.AreEqual<int>(6, MathUtil.ValidateNumber("6", 5, 10));
      Assert.AreEqual<int>(5, MathUtil.ValidateNumber("4", 5, 10));
      Assert.AreEqual<int>(10, MathUtil.ValidateNumber("11", 5, 10));      
    }

    [TestMethod]
    public void ShouldIncrementStringNumber()
    {
      Assert.AreEqual<int>(1, MathUtil.IncrementDecrementNumber("wrong", 0, 5, true));
      Assert.AreEqual<int>(2, MathUtil.IncrementDecrementNumber("1", 0, 3, true));      
    }

    [TestMethod]
    public void ShouldDecrementStringNumber()
    {
      Assert.AreEqual<int>(0, MathUtil.IncrementDecrementNumber("wrong", -1, 0, false));
      Assert.AreEqual<int>(1, MathUtil.IncrementDecrementNumber("2", 0, 3, false));
    }
  }
}
