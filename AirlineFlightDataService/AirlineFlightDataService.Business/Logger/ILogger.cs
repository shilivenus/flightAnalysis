using System;
using System.Collections.Generic;
using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Logger
{
    public interface ILogger
    {
        void LogEventInfo(EventDetails eventDetails, TimeSpan timeSpent);
        void LogEventDetails(Dictionary<string, int> eventDetailsList);
        void LogBatchProcessTime(TimeSpan timeSpent);
        void LogFailedEventDetails(List<string> failedEventList, int totalFailed);
        void LogInfoToConsole(string infoMessage);
        void LogErrorToConsole(Exception e, string errorMessage);
    }
}