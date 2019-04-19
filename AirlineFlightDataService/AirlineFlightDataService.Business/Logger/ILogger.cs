using System;
using System.Collections.Generic;
using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Logger
{
    /// <summary>
    /// Represents a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log List of event types that were processed, including count of events per type.
        /// Totoal duration for processing each batch.
        /// Count of failed events.
        /// List of the IDs of the failed events.
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <param name="timeSpent"></param>
        void LogEventInfo(EventDetails eventDetails, TimeSpan timeSpent);

        /// <summary>
        /// Log event details.
        /// </summary>
        /// <param name="eventDetailsList">Used to determian which message should be logged</param>
        void LogEventDetails(Dictionary<string, int> eventDetailsList);

        /// <summary>
        /// Log the total time spent.
        /// </summary>
        /// <param name="timeSpent"></param>
        void LogBatchProcessTime(TimeSpan timeSpent);

        /// <summary>
        /// Log the information for failed events.
        /// </summary>
        /// <param name="failedEventList"></param>
        /// <param name="totalFailed"></param>
        void LogFailedEventDetails(List<string> failedEventList, int totalFailed);

        /// <summary>
        /// Log information message to console.
        /// </summary>
        /// <param name="infoMessage"></param>
        void LogInfoToConsole(string infoMessage);

        /// <summary>
        /// Log error details to console.
        /// </summary>
        /// <param name="e">Exception to be logged</param>
        /// <param name="errorMessage">Exception error message to be logged</param>
        void LogErrorToConsole(Exception e, string errorMessage);
    }
}