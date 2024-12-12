using FakeItEasy;

using NUnit.Framework;

namespace CsvReader.Core.Interactions
{
  [TestFixture]
  public class JumpToPageSpecs
  {
    [Test]
    public void ShouldAskForPageNumber()
    {
      var output = A.Fake<IConsole>();
      var jtp = new JumpToPage(output);
      jtp.GetCommand(new PagedModel(new Model(), 0, 1));

      A
        .CallTo(() => output.ReadLine())
        .MustHaveHappened();
    }

    [TestCase("-1", 0)]
    [TestCase("4", 2)]
    [TestCase("a", 1)]
    [TestCase("2", 1)]
    public void ShouldYieldExpectedPageIndexForUserInput(string userInput, int expectedNewPageIndex)
    {
      var model = new Model()
        .SetHeader(new[] { "header" })
        .AddRow(new[] { "one" })
        .AddRow(new[] { "two" })
        .AddRow(new[] { "three" });

      const int CurrentPage = 1;
      var paged = new PagedModel(model, CurrentPage, 1);

      var output = A.Fake<IConsole>();
      A
        .CallTo(() => output.ReadLine())
        .Returns(userInput);

      var jtp = new JumpToPage(output);
      var newPageIndex = jtp.GetCommand(paged).GetNextPageIndex();

      Assert.AreEqual(expectedNewPageIndex, newPageIndex);
    }
  }
}