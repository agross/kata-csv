using NUnit.Framework;

namespace CsvReader.Core.Interactions
{
  [TestFixture]
  public class FirstPageTests
  {
    [Test]
    public void ShouldReturnZeroAsNewPageIndex()
    {
      var newPageIndex = new FirstPage().GetCommand(null, 0).Execute();

      Assert.AreEqual(0, newPageIndex);
    }
  }
}