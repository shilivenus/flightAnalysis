using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    public interface ILogger
    {
        void LogEventDetails(Dictionary<string, int> eventDetailsList);
        void LogBatchProcessTime(TimeSpan timeSpent);
        void LogFailedEventDetails(List<string> failedEventList, int totalFailed);
    }
}