using System.Collections.Generic;

namespace AirlineFlightDataService.Module
{
    public class EventReaderResult
    {
        public List<Event> Events { get; set; }
        public List<string> Errors { get; set; }

        public EventReaderResult(List<Event> events, List<string> errors)
        {
            Events = events;
            Errors = errors;
        }
    }
}
