namespace CsvReader.Core.Interactions
{
  abstract class PageInteraction
  {
    protected static int EnsurePageIndexRange(PagedModel model, int currentPageIndex)
    {
      var result = currentPageIndex;

      if (result > model.MaxPageIndex)
      {
        result = model.MaxPageIndex;
      }

      if (result < 0)
      {
        result = 0;
      }

      return result;
    }
  }
}