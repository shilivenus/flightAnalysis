using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Reader
{
    public interface IEventReader
    {
        EventReaderResult Read(string filePath);
    }
}