namespace CsvReader.Core
{
  public interface IConsole
  {
    void Write(string output, params object[] args);
    void WriteLine(string output, params object[] args);
    char Read();
    string ReadLine();
    void Clear();
  }
}