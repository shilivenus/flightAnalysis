using System.Collections.Generic;
using System.IO;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Reader
{
    public class EventReader : IEventReader
    {
        private readonly ILogger _logger;

        public EventReader(ILogger logger)
        {
            _logger = logger;
        }

        public List<Event> Read(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(filePath));
        }
    }
}