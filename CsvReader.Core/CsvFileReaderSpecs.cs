using System.Text;

using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class FileReaderSpecs
  {
    [Test]
    public void HeaderOnly_Should_YieldNoRows()
    {
      var input = "Name;Age;City";
      var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

      var model = new CsvFileReader().Read(stream);

      Assert.That(model.Header.Count(), Is.EqualTo(3));
      Assert.That(model.Header.First(), Is.EqualTo("Name"));
      Assert.That(model.Header.Skip(1).First(), Is.EqualTo("Age"));
      Assert.That(model.Header.Skip(2).First(), Is.EqualTo("City"));
      Assert.That(model.Rows, Is.Empty);
    }

    [Test]
    public void HeaderAndRows_Should_YieldAllRows()
    {
      var input = "Name;Age;City\r\nAlex;31;Leipzig\r\nClaudia;25;Leipzig";
      var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

      var model = new CsvFileReader().Read(stream);

      Assert.That(model.Header.Count(), Is.EqualTo(3));
      Assert.That(model.Rows.Count(), Is.EqualTo(2));

      var alex = model.Rows.First();
      Assert.That(alex.First(), Is.EqualTo("Alex"));
      Assert.That(alex.Skip(1).First(), Is.EqualTo("31"));
      Assert.That(alex.Skip(2).First(), Is.EqualTo("Leipzig"));
    }
  }
}
