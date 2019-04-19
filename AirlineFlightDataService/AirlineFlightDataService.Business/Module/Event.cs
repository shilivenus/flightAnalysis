using System;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Business.Module
{
    public class Event
    {
        [JsonProperty("eventType")]
        public string EventType { get; set; }

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
