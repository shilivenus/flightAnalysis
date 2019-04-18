namespace AirlineFlightDataService.Processor
{
    public interface IEventProcessor
    {
        void Process(string filePath, string fileName);
    }
}