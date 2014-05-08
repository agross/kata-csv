using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class LastPage : PageInteraction, IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "L";
    }

    public ICommand GetCommand(PagedModel model)
    {
      return new SetCurrentPageCommand(model.MaxPageIndex);
    }
  }
}