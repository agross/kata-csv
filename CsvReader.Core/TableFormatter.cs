using System.Linq;
using System.Text;

namespace CsvReader.Core
{
  public class TableFormatter : IFormatter
  {
    public string Format(Model model)
    {
      var widths = model.Header
        .Select((c, index) => Model.AllValuesForColumn(index, model))
        .Select(colValues => colValues.Max(x => x.Length))
        .ToArray();

      var result = new StringBuilder();

      RenderHeader(model, widths, result);
      RenderRows(model, widths, result);

      return result.ToString();
    }

    static void RenderRows(Model model, int[] widths, StringBuilder result)
    {
      foreach (var row in model.Rows)
      {
        for (var i = 0; i < model.Header.Count(); i++)
        {
          result.Append(row.Skip(i).First().PadRight(widths.Skip(i).First())).Append("|");
        }
        result.AppendLine();
      }
    }

    static void RenderHeader(Model model, int[] widths, StringBuilder result)
    {
      for (var i = 0; i < model.Header.Count(); i++)
      {
        result.Append(model.Header.Skip(i).First().PadRight(widths.Skip(i).First())).Append("|");
      }
      result.AppendLine();

      for (var i = 0; i < model.Header.Count(); i++)
      {
        result.Append(new string('-', widths.Skip(i).First())).Append("+");
      }
      result.AppendLine();
    }
  }

}