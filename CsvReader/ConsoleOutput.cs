using System;
using System.IO;

using CsvReader.Core;

namespace CsvReader
{
  class ConsoleOutput : IOutput
  {
    public void Write(string output, params object[] args)
    {
      Console.Write(output, args);
    }
    
    public void WriteLine(string output, params object[] args)
    {
      Console.WriteLine(output, args);
    }

    public char Read()
    {
      return Console.ReadKey().KeyChar;
    }

    public void Clear()
    {
      Console.Clear();
    }
  }
}