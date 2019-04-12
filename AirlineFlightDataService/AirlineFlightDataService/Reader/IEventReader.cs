using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Reader
{
    public interface IEventReader
    {
        List<Event> Read(string filePath);
    }
}