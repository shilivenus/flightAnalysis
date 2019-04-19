using System;
using System.Collections.Generic;
using AirlineFlightDataService.LogWriter;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    public class FlightEventLogger : ILogger
    {
        private readonly ILogWriter _logWriter;

        public FlightEventLogger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

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

        public void LogBatchProcessTime(TimeSpan timeSpent)
        {
            _logWriter.WriteInfoToConsole($"This batch takes {timeSpent} in total");
        }

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

        public void LogInfoToConsole(string infoMessage)
        {
            _logWriter.WriteInfoToConsole(infoMessage);
        }

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
