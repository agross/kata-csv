using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class PreviousPage : PageInteraction, IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "P";
    }

    public ICommand GetCommand(PagedModel model)
    {
      var previousPage = model.PageIndex - 1;

      previousPage = EnsurePageIndexRange(model, previousPage);

      return new SetCurrentPageCommand(previousPage);
    }
  }
}