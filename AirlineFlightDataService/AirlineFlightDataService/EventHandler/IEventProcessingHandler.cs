using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.EventHandler
{
    public interface IEventProcessingHandler
    {
        EventDetails ProcessingEvent(EventDetails eventDetails, EventReaderResult result);
    }
}