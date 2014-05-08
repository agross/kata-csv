using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  class NoOp : IInteraction
  {
    public bool CanHandle(string userInput)
    {
      return true;
    }

    public ICommand GetCommand(PagedModel model)
    {
      return new SetCurrentPageCommand(model.PageIndex);
    }
  }
}