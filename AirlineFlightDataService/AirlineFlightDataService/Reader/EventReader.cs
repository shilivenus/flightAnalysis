using System.Collections.Generic;
using System.IO;
using AirlineFlightDataService.Module;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Reader
{
    public class EventReader : IEventReader
    {
        public EventReaderResult Read(string filePath)
        {
            List<string> errors = new List<string>();

            var settings = new JsonSerializerSettings()
            {
                Error = (s, e) => {
                    errors.Add(e.ErrorContext.Error.Message);
                    e.ErrorContext.Handled = true;
                }
            };

            var events = JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(filePath), settings);

            return new EventReaderResult(events, errors);
        }
    }
}