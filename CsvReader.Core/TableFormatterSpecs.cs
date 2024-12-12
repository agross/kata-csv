using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class TableFormatterSpecs
  {
    [Test]
    public void CanRenderSimpleTable()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });

      var output = new TableFormatter().Format(model);

      Assert.AreEqual(@"Foo |Bar |
----+----+
1   |2   |
1111|2222|
", output);
    }

    [Test]
    public void CanRenderPagedTable_Page0()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });

      var page1 = new PagedModel(model, 0, 1);

      var output = new TableFormatter().Format(page1);

      Assert.AreEqual(@"Foo|Bar|
---+---+
1  |2  |
", output);
    }

    [Test]
    public void CanRenderPagedTable_Page1()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });

      var page1 = new PagedModel(model, 1, 1);

      var output = new TableFormatter().Format(page1);

      Assert.AreEqual(@"Foo |Bar |
----+----+
1111|2222|
", output);
    }

    [Test]
    public void CanRenderLastPageFor3ItemsAndPageSize2()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "11", "22" });

      var page1 = new PagedModel(model, 1, 2);

      var output = new TableFormatter().Format(page1);

      Assert.AreEqual(@"Foo|Bar|
---+---+
11 |22 |
", output);
    }
  }
}