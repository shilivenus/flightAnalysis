namespace AirlineFlightDataService.Business.Processor
{
    public interface IEventProcessor
    {
        void Process(string filePath, string fileName);
    }
}