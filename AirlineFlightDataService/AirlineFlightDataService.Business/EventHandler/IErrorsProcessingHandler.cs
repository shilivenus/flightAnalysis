using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.EventHandler
{
    public interface IErrorsProcessingHandler
    {
        void ProcessingErrors(string filePath, string fileName, EventReaderResult result);
    }
}