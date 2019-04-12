using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    class EventLogger : ILogger
    {
        public void LogEventDetails(List<Event> events)
        {
            var processedEventType = events.Select(e => e.EventType).Distinct().ToList();
        }

        public void LogBatchProcessTime(DateTime starTime)
        {
            throw new NotImplementedException();
        }

        public void LogFailedEventDetails(List<Event> failedEvents)
        {
            throw new NotImplementedException();
        }
    }
}
