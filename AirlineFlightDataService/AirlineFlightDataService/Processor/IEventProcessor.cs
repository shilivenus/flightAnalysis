using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Processor
{
    public interface IEventProcessor
    {
        void Process(string filePath, PathConfiguration pathConfiguration);
    }
}