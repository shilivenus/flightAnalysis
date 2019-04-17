using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    public interface ILogger
    {
        void LogEventInfo(EventDetails eventDetails, TimeSpan timeSpent);
    }
}