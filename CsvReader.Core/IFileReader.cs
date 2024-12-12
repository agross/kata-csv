namespace CsvReader.Core
{
  public interface IFileReader
  {
    Model Read(string fileName);
  }
}