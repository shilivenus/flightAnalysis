using System.Collections.Generic;

namespace AirlineFlightDataService.Module
{
    public class EventDetails
    {
        public EventDetails(Dictionary<string, int> eventDetailsList, List<string> failedEventList, int failedEventCount)
        {
            EventDetailsList = eventDetailsList;
            FailedEventList = failedEventList;
            FailedEventCount = failedEventCount;
        }

        public Dictionary<string, int> EventDetailsList { get; set; }
        public List<string> FailedEventList { get; set; }
        public int FailedEventCount { get; set; }
    }
}
