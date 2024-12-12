using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class PagedModelSpecs
  {
    [Test]
    public void ShouldYieldNumberOfPages()
    {
      var model = new Model();
      model.SetHeader(new[] { "Foo", "Bar" });
      model.AddRow(new[] { "1", "2" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "1111", "2222" });
      model.AddRow(new[] { "1111", "2222" });

      var paged = new PagedModel(model, 0, 2);

      Assert.AreEqual(3, paged.MaxPageIndex);
    }
  }
}
