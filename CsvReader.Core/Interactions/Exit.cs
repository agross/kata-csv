using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class Exit : IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "X";
    }

    public ICommand GetCommand(PagedModel model, int currentPageIndex)
    {
      return new ExitApplicationCommand();
    }
  }
}