using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Reader
{
    public interface IEventReader
    {
        EventReaderResult Read(string filePath);
    }
}