using NUnit.Framework;

namespace CsvReader.Core.Interactions
{
  [TestFixture]
  public class FirstPageTests
  {
    [Test]
    public void ShouldReturnZeroAsNewPageIndex()
    {
      var newPageIndex = new FirstPage().GetCommand(null).GetNextPageIndex();

      Assert.That(newPageIndex, Is.EqualTo(0));
    }
  }
}
