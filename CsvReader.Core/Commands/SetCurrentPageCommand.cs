namespace CsvReader.Core.Commands
{
  class SetCurrentPageCommand : ICommand
  {
    readonly int _pageIndex;

    public SetCurrentPageCommand(int pageIndex)
    {
      _pageIndex = pageIndex;
    }

    public int Execute()
    {
      return _pageIndex;
    }
  }
}