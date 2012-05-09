using System.Collections.Generic;
using System.Linq;

namespace CsvReader.Core
{
  public class Model
  {
    IEnumerable<string> _header;
    readonly IList<IEnumerable<string>> _rows;

    public Model()
    {
      _header = Enumerable.Empty<string>();
      _rows = new List<IEnumerable<string>>();
    }

    public virtual IEnumerable<string> Header
    {
      get
      {
        return _header;
      }
    }

    public virtual IEnumerable<IEnumerable<string>> Rows
    {
      get
      {
        return _rows;
      }
    }

    public virtual Model AddRow(IEnumerable<string> items)
    {
      _rows.Add(items);
      return this;
    }

    public virtual Model SetHeader(IEnumerable<string> items)
    {
      _header = items;
      return this;
    }

    public static IEnumerable<string> AllValuesForColumn(int columnIndex, Model model)
    {
      yield return model.Header.Skip(columnIndex).First();

      foreach (var row in model.Rows)
      {
        yield return row.Skip(columnIndex).First();
      }
    }
  }
}
