using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class ModelSpecs
  {
    [Test]
    public void AllValues_Should_YieldAllValues()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });

      var column1 = Model.AllValuesForColumn(0, model);
      var column2 = Model.AllValuesForColumn(1, model);

      CollectionAssert.AreEqual(column1, new [] { "Foo","1", "1111" });
      CollectionAssert.AreEqual(column2, new [] { "Bar","2", "2222" });
    }
  }
}