using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvReader.Core
{
  public class NumberedModel : Model
  {
    readonly Model _model;

    public NumberedModel(Model model)
    {
      _model = model;
    }

    public override IEnumerable<string> Header
    {
      get
      {
        return new []{"No."}.Concat(_model.Header);
      }
    }

    public override IEnumerable<IEnumerable<string>> Rows
    {
      get
      {
        return _model.Rows
          .Select((row, index) => new[] { String.Format("{0}.", index + 1) }.Concat(row));
      }
    }
  }
}