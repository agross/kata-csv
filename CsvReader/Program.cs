﻿using CsvReader.Core;

namespace CsvReader
{
  class Program
  {
    static void Main(string[] args)
    {
      var coordinator = new DisplayCoordinator(new CsvFileReader(), new TableFormatter(), new ConsoleOutput());
      coordinator.Display("persons.csv", 3);
    }
  }
}
