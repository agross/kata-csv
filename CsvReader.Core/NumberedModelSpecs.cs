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

      Assert.That(numbered.Header.Count(), Is.EqualTo(2));
      Assert.That(numbered.Rows.First().Count(), Is.EqualTo(2));

      Assert.That(numbered.Header.First(), Is.EqualTo("No."));
      Assert.That(numbered.Rows.First().First(), Is.EqualTo("1."));
      Assert.That(numbered.Rows.Skip(1).First().First(), Is.EqualTo("2."));
    }
  }
}
