using System;
using System.Collections.Generic;
using System.Text;

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
