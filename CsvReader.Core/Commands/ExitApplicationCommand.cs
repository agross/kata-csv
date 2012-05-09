using System;

namespace CsvReader.Core.Commands
{
  class ExitApplicationCommand : ICommand
  {
    public int Execute()
    {
      return -1;
    }
  }
}