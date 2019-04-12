using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Logger
{
    class EventLogger : ILogger
    {
        public void LogEventDetails(Dictionary<string, int> eventDetailsList)
        {
            throw new NotImplementedException();
        }

        public void LogBatchProcessTime(TimeSpan timeSpent)
        {
            throw new NotImplementedException();
        }

        public void LogFailedEventDetails(List<string> failedEventList, int totalFailed)
        {
            throw new NotImplementedException();
        }
    }
}
