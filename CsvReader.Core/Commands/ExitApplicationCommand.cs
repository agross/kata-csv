using System;

namespace CsvReader.Core.Commands
{
  class ExitApplicationCommand : ICommand
  {
    public int? GetNextPageIndex()
    {
      return null;
    }
  }
}