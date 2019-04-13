using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    class EventLogger : ILogger
    {
        public void LogEventInfo(Dictionary<string, int> eventDetailsList, TimeSpan timeSpent, List<string> failedEventList, int totalFailed)
        {
            LogEventDetails(eventDetailsList);
            LogBatchProcessTime(timeSpent);
            LogFailedEventDetails(failedEventList, totalFailed);
        }

        private void LogEventDetails(Dictionary<string, int> eventDetailsList)
        {
            foreach(var eventDetail in eventDetailsList)
            {
                Console.WriteLine($"{eventDetail.Key} has {eventDetail.Value}");
            }
        }

        private void LogBatchProcessTime(TimeSpan timeSpent)
        {
            Console.WriteLine($"This batch takes {timeSpent} in total");
        }

        private void LogFailedEventDetails(List<string> failedEventList, int totalFailed)
        {
            if(totalFailed == 0)
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
    }
}
