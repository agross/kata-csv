using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class NextPage : PageInteraction, IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return userInput == "N";
    }

    public ICommand GetCommand(PagedModel model)
    {
      var nextPage = model.PageIndex + 1;

      nextPage = EnsurePageIndexRange(model, nextPage);

      return new SetCurrentPageCommand(nextPage);
    }
  }
}