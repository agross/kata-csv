using CsvReader.Core.Commands;

namespace CsvReader.Core.Interactions
{
  interface IInteraction
  {
    bool CanHandle(string userInput);
    ICommand GetCommand(PagedModel model);
  }
}