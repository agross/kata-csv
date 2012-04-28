namespace CsvReader.Core
{
  public interface IOutput
  {
    void Write(string output);
    char Read();
  }
}