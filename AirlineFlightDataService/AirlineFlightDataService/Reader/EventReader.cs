using System.Collections.Generic;
using System.IO;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Reader
{
    public class EventReader : IEventReader
    {
        public List<Event> Read(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(filePath));
        }
    }
}