using System;

namespace AirlineFlightDataService.LogWriter
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void WriteToConsole(string input)
        {
            Console.WriteLine(input);
        }
    }
}
