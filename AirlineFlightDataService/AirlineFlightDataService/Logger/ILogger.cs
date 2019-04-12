using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    public interface ILogger
    {
        void LogEventDetails(List<Event> events);
        void LogBatchProcessTime(DateTime starTime);
        void LogFailedEventDetails(List<Event> failedEvents);
    }
}