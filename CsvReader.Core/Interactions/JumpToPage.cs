using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class JumpToPage : PageInteraction, IInteraction
  {
    readonly IOutput _output;

    public JumpToPage(IOutput output)
    {
      _output = output;
    }

    public bool CanHandle(string userInput)
    {
      return userInput == "J";
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      _output.Write("Page? ");
      var page = _output.ReadLine();

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