using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    public class EventLogger : ILogger
    {
        public void LogEventDetails(Dictionary<string, int> eventDetailsList)
        {
            if(eventDetailsList.Count == 0)
            {
                Console.WriteLine("There is no event been processed.");
            }
            else
            {
                foreach (var eventDetail in eventDetailsList)
                {
                    Console.WriteLine($"{eventDetail.Key} has {eventDetail.Value}");
                }
            }
        }

        public void LogBatchProcessTime(TimeSpan timeSpent)
        {
            Console.WriteLine($"This batch takes {timeSpent} in total");
        }

        public void LogFailedEventDetails(List<string> failedEventList, int totalFailed)
        {
            if(totalFailed == 0 || failedEventList.Count == 0)
            {
                Console.WriteLine("There is no failed event");
            }
            else
            {
                Console.WriteLine($"Total Failed number is {totalFailed}");
                Console.WriteLine("Failed events have been listed below");

                foreach (var failedEvent in failedEventList)
                {
                    Console.WriteLine(failedEvent);
                }
            }
        }

        public void LogEventInfo(EventDetails eventDetails, TimeSpan timeSpent)
        {
            LogEventDetails(eventDetails.EventDetailsList);
            LogBatchProcessTime(timeSpent);
            LogFailedEventDetails(eventDetails.FailedEventList, eventDetails.FailedEventCount);
        }
    }
}
