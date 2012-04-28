using System.IO;
using System.Linq;
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

      Assert.AreEqual(3, model.Header.Count());
      Assert.AreEqual("Name", model.Header.First());
      Assert.AreEqual("Age", model.Header.Skip(1).First());
      Assert.AreEqual("City", model.Header.Skip(2).First());
      Assert.IsEmpty(model.Rows);
    }

    [Test]
    public void HeaderAndRows_Should_YieldAllRows()
    {
      var input = "Name;Age;City\r\nAlex;31;Leipzig\r\nClaudia;25;Leipzig";
      var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

      var model = new CsvFileReader().Read(stream);

      Assert.AreEqual(3, model.Header.Count());
      Assert.AreEqual(model.Rows.Count(), 2);

      var alex = model.Rows.First();
      Assert.AreEqual("Alex", alex.First());
      Assert.AreEqual("31", alex.Skip(1).First());
      Assert.AreEqual("Leipzig", alex.Skip(2).First());
    }
  }
}
