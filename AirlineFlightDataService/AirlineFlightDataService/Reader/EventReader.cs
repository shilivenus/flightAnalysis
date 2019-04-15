using System.Collections.Generic;
using System.IO;
using AirlineFlightDataService.Module;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Reader
{
    public class EventReader : IEventReader
    {
        public List<Event> Read(string filePath, List<string> errors)
        {
            
            var settings = new JsonSerializerSettings()
            {
                Error = (s, e) => {
                    errors.Add(e.ErrorContext.Error.Message);
                    e.ErrorContext.Handled = true;
                }
            };

            return JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(filePath), settings);
        }
    }
}