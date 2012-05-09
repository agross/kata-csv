using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class JumpToPage : PageInteraction, IInteraction
  {
    readonly IConsole _console;

    public JumpToPage(IConsole console)
    {
      _console = console;
    }

    public bool CanHandle(string userInput)
    {
      return userInput == "J";
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      _console.Write("Page? ");
      var page = _console.ReadLine();

      int newPageIndex;
      if(int.TryParse(page, out newPageIndex))
      {
        newPageIndex = EnsurePageIndexRange(model, newPageIndex - 1);
      }
      else
      {
        newPageIndex = currentPageIndex;
      }
      return new SetCurrentPageCommand(newPageIndex);
    }
  }
}