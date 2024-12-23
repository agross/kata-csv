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
    readonly IConsole _console;
    readonly IEnumerable<IInteraction> _possibleInteractions = Enumerable.Empty<IInteraction>();


    public DisplayCoordinator(IFileReader fileReader, IFormatter formatter, IConsole console)
    {
      _possibleInteractions = new IInteraction[]
                             {
                               new Exit(),
                               new NextPage(),
                               new PreviousPage(),
                               new LastPage(),
                               new FirstPage(),
                               new JumpToPage(console),
                               new NoOp()
                             };
      _fileReader = fileReader;
      _formatter = formatter;
      _console = console;
    }

    public void Display(string file, int pageSize)
    {
      var modelFromFile = _fileReader.Read(file);
      var model = new NumberedModel(modelFromFile);

      var pageIndex = 0;
      while (true)
      {
        var pagedModel = new PagedModel(model, pageIndex, pageSize);
        
        Display(pagedModel);

        var userInput = _console.Read();

        var nextAction = DetermineNextAction(pagedModel, userInput);
        var maybePageIndex = nextAction.GetNextPageIndex();
        if (maybePageIndex == null)
        {
          return;
        }
        pageIndex = maybePageIndex.Value;
      }
    }

    void Display(PagedModel pagedModel)
    {
      var formatted = _formatter.Format(pagedModel);
      _console.Clear();
      _console.Write(formatted);
      _console.WriteLine("Page {0} of {1}", pagedModel.PageIndex + 1, pagedModel.MaxPageIndex + 1);
      _console.WriteLine(String.Empty);
      _console.Write("N(ext page, P(revious page, F(irst page, L(ast page, J(ump to page, eX(it");
    }

    ICommand DetermineNextAction(PagedModel pagedModel, char userInput)
    {
      return _possibleInteractions
        .First(x => x.CanHandle(userInput.ToString().ToUpperInvariant()))
        .GetCommand(pagedModel);
    }
  }
}
