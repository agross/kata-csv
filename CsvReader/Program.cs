using CsvReader;
using CsvReader.Core;

var coordinator = new DisplayCoordinator(new CsvFileReader(), new TableFormatter(), new ConsoleConsole());
coordinator.Display("persons.csv", 3);
