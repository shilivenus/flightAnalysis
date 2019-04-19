using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.EventHandler
{
    /// <summary>
    /// Represents an event processing handler.
    /// </summary>
    public interface IEventProcessingHandler
    {
        /// <summary>
        /// Processing each event and return EventDetails object.
        /// </summary>
        /// <param name="eventDetails">Used to fill in event details</param>
        /// <param name="result">Used to get each event</param>
        /// <returns>Returns EventDetails object</returns>
        EventDetails ProcessingEvent(EventDetails eventDetails, EventReaderResult result);
    }
}