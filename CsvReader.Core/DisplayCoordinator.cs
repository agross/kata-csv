using System;
using System.Collections.Generic;
using System.Linq;

using CsvReader.Core.Commands;
using CsvReader.Core.Interactions;

namespace CsvReader.Core
{
  public class DisplayCoordinator
  {
    readonly IFileReader _fileReader;
    readonly IFormatter _formatter;
    readonly IOutput _output;
    readonly IEnumerable<IInteraction> _possibleInteractions = Enumerable.Empty<IInteraction>();


    public DisplayCoordinator(IFileReader fileReader, IFormatter formatter, IOutput output)
    {
      _possibleInteractions = new IInteraction[]
                             {
                               new Exit(),
                               new NextPage(),
                               new PreviousPage(),
                               new LastPage(),
                               new FirstPage(),
                               new JumpToPage(output),
                               new NoOp()
                             };
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
        _output.WriteLine(String.Empty);
        _output.Write("N(ext page, P(revious page, F(irst page, L(ast page, J(ump to page, eX(it");
       
        var userInput = _output.Read();

        var nextAction = DetermineNextAction(pagedModel, userInput, pageIndex);
        pageIndex = nextAction.Execute();
        if (pageIndex < 0)
        {
          return;
        }
      }
    }

    ICommand DetermineNextAction(PagedModel pagedModel, char action, int currentPageIndex)
    {
      return _possibleInteractions
        .First(x => x.CanHandle(action.ToString().ToUpperInvariant()))
        .GetCommand(pagedModel, currentPageIndex);
    }
  }
}
