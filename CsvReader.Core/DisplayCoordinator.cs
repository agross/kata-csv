using System;

namespace CsvReader.Core
{
  public class DisplayCoordinator
  {
    readonly IFileReader _fileReader;
    readonly IFormatter _formatter;
    readonly IOutput _output;

    public DisplayCoordinator(IFileReader fileReader, IFormatter formatter, IOutput output)
    {
      _fileReader = fileReader;
      _formatter = formatter;
      _output = output;
    }

    public void Display(string file, int pageSize)
    {
      var model = new NumberedModel(_fileReader.Read(file));

      var pageIndex = 0;
      while (true)
      {
        var pagedModel = new PagedModel(model, pageIndex, pageSize);
        var formatted = _formatter.Format(pagedModel);

        _output.Clear();
        _output.Write(formatted);
        _output.WriteLine("Page {0} of {1}", pageIndex + 1, pagedModel.MaxPageIndex + 1);
        _output.WriteLine(string.Empty);
        _output.Write("N(ext page, P(revious page, F(irst page, L(ast page, eX(it");
        var action = _output.Read();

        switch (action.ToString().ToUpperInvariant())
        {
          case "N":
            pageIndex += 1;
            break;

          case "P":
            pageIndex -= 1;
            break;

          case "F":
            pageIndex = 0;
            break;

          case "L":
            pageIndex = pagedModel.MaxPageIndex;
            break;

          case "X":
            return;
        }

        if (pageIndex < 0)
        {
          pageIndex = 0;
        }

        if (pageIndex > pagedModel.MaxPageIndex)
        {
          pageIndex = pagedModel.MaxPageIndex;
        }
      }
    }
  }
}
