using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class PreviousPage : PageInteraction, IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "P";
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      currentPageIndex = currentPageIndex - 1;

      currentPageIndex = EnsurePageIndexRange(model, currentPageIndex);

      return new SetCurrentPageCommand(currentPageIndex);
    }
  }
}