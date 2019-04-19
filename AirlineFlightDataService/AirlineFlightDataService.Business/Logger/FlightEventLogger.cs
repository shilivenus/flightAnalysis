using System;
using System.Collections.Generic;
using AirlineFlightDataService.Business.LogWriter;
using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Logger
{
    /// <summary>
    /// Log Information or error into console or file.
    /// </summary>
    public class FlightEventLogger : ILogger
    {
        private readonly ILogWriter _logWriter;

        public FlightEventLogger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        /// <summary>
        /// Log event details.
        /// </summary>
        /// <param name="eventDetailsList">Used to determian which message should be logged</param>
        public void LogEventDetails(Dictionary<string, int> eventDetailsList)
        {
            if(eventDetailsList.Count == 0)
            {
                _logWriter.WriteInfoToConsole("There is no event been processed.");
            }
            else
            {
                foreach (var eventDetail in eventDetailsList)
                {
                    _logWriter.WriteInfoToConsole($"{eventDetail.Key} has {eventDetail.Value}");
                }
            }
        }

        /// <summary>
        /// Log the total time spent.
        /// </summary>
        /// <param name="timeSpent"></param>
        public void LogBatchProcessTime(TimeSpan timeSpent)
        {
            _logWriter.WriteInfoToConsole($"This batch takes {timeSpent} in total");
        }

        /// <summary>
        /// Log the information for failed events.
        /// </summary>
        /// <param name="failedEventList"></param>
        /// <param name="totalFailed"></param>
        public void LogFailedEventDetails(List<string> failedEventList, int totalFailed)
        {
            if(totalFailed == 0 || failedEventList.Count == 0)
            {
                _logWriter.WriteInfoToConsole("There is no failed event");
            }
            else
            {
                _logWriter.WriteInfoToConsole($"Total Failed number is {totalFailed}");
                _logWriter.WriteInfoToConsole("Failed events have been listed below");

                foreach (var failedEvent in failedEventList)
                {
                    _logWriter.WriteInfoToConsole(failedEvent);
                }
            }
        }

        /// <summary>
        /// Log information message to console.
        /// </summary>
        /// <param name="infoMessage"></param>
        public void LogInfoToConsole(string infoMessage)
        {
            _logWriter.WriteInfoToConsole(infoMessage);
        }

        /// <summary>
        /// Log error details to console.
        /// </summary>
        /// <param name="e">Exception to be logged</param>
        /// <param name="errorMessage">Exception error message to be logged</param>
        public void LogErrorToConsole(Exception e, string errorMessage)
        {
            _logWriter.WriteErrorToConsole(e, errorMessage);
        }

        /// <summary>
        /// Log List of event types that were processed, including count of events per type.
        /// Totoal duration for processing each batch.
        /// Count of failed events.
        /// List of the IDs of the failed events.
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <param name="timeSpent"></param>
        public void LogEventInfo(EventDetails eventDetails, TimeSpan timeSpent)
        {
            LogEventDetails(eventDetails.EventDetailsList);
            LogBatchProcessTime(timeSpent);
            LogFailedEventDetails(eventDetails.FailedEventList, eventDetails.FailedEventCount);
        }
    }
}
