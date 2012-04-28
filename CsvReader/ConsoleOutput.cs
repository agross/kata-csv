using System;

using CsvReader.Core;

namespace CsvReader
{
  class ConsoleOutput : IOutput
  {
    public void Write(string output)
    {
      Console.WriteLine(output);
    }

    public char Read()
    {
      return Console.ReadKey().KeyChar;
    }
  }
}