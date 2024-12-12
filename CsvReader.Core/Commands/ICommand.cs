using System;

namespace CsvReader.Core.Commands
{
  interface ICommand
  {
    Nullable<int> GetNextPageIndex();
  }
}