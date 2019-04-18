using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.EventHandler
{
    public interface IErrorsProcessingHandler
    {
        void ProcessingErrors(string filePath, string fileName, EventReaderResult result);
    }
}