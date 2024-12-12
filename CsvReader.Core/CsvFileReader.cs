using System;
using System.Collections.Generic;
using System.IO;

namespace CsvReader.Core
{
  public class CsvFileReader : IFileReader
  {
    public Model Read(string fileName)
    {
      return Read(new FileStream(fileName, FileMode.Open));
    }

    public Model Read(Stream input)
    {
      var result = new Model();

      using (var reader = new StreamReader(input))
      {
        Action<IEnumerable<string>> addRow = items => result.AddRow(items);
        Action<IEnumerable<string>> add = items => result.SetHeader(items);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var items = line.Split(';');
          add(items);
          add = addRow;
        }
      }

      return result;
    }
  }
}