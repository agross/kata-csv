using System.Linq;

using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class NumberedModelSpecs
  {
    [Test]
    public void RowsOfNumbredModel_Should_ContainNumbers()
    {
      var model = new Model()
      .SetHeader(new[] { "header" })
        .AddRow(new[] { "one" })
        .AddRow(new[] { "two" });

      var numbered = new NumberedModel(model);

      Assert.AreEqual(2, numbered.Header.Count());
      Assert.AreEqual(2, numbered.Rows.First().Count());

      Assert.AreEqual("No.", numbered.Header.First());
      Assert.AreEqual("1.", numbered.Rows.First().First());
      Assert.AreEqual("2.", numbered.Rows.Skip(1).First().First());
    }
  }
}