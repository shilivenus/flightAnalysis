using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.EventHandler
{
    public interface IEventProcessingHandler
    {
        EventDetails ProcessingEvent(EventDetails eventDetails, EventReaderResult result);
    }
}