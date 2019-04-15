using System;
using AirlineFlightDataService.Enum;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Module
{
    public class Event
    {
        [JsonProperty("eventType")]
        public EventType EventType { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("flight")]
        public string Flight { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("passengers")]
        public int Passengers { get; set; }

        [JsonProperty("delayed")]
        public string Delayed { get; set; }
    }
}
