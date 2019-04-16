using AirlineFlightDataService.Configuration;

namespace AirlineFlightDataService.Processor
{
    public interface IEventProcessor
    {
        void Process(string filePath, string fileName, IFilePathConfiguration pathConfiguration);
    }
}