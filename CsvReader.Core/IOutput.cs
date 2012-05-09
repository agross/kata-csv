namespace CsvReader.Core
{
  public interface IOutput
  {
    void Write(string output, params object[] args);
    void WriteLine(string output, params object[] args);
    char Read();
    string ReadLine();
    void Clear();
  }
}