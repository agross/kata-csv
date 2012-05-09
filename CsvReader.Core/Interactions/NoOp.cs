using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class NoOp : IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return true;
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      return new SetCurrentPageCommand(currentPageIndex);
    }
  }
}