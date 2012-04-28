using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvReader.Core
{
  public class PagedModel : Model
  {
    readonly Model _model;
    readonly int _pageIndex;
    readonly int _pageSize;

    public PagedModel(Model model, int pageIndex, int pageSize)
    {
      _model = model;
      _pageIndex = pageIndex;
      _pageSize = pageSize;
    }

    public override IEnumerable<string> Header
    {
      get
      {
        return _model.Header;
      }
    }

    public override IEnumerable<IEnumerable<string>> Rows
    {
      get
      {
        return _model.Rows.Skip(_pageIndex * _pageSize).Take(_pageSize);
      }
    }

    public int MaxPageIndex
    {
      get
      {
        return (int) Math.Ceiling(_model.Rows.Count() / (double) _pageSize) - 1;
      }
    }

    public override void AddRow(IEnumerable<string> items)
    {
      throw new NotSupportedException();
    }

    public override void SetHeader(IEnumerable<string> items)
    {
      throw new NotSupportedException();
    }
  }
}