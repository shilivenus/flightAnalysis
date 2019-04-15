using System;
using System.Collections.Generic;

namespace AirlineFlightDataService.Logger
{
    public interface ILogger
    {
        void LogEventInfo(Dictionary<string, int> eventDetailsList, TimeSpan timeSpent, List<string> failedEventList, int totalFailed);
    }
}