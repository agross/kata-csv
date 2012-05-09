using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class FirstPage : IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "F";
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      currentPageIndex = 0;

      return new SetCurrentPageCommand(currentPageIndex);
    }
  }
}