using System.Collections.Generic;
using System.IO;
using AirlineFlightDataService.Business.Module;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Business.Reader
{
    /// <summary>
    /// Read file from file path provided, deserializing the file into List of events.
    /// Doing error handlering, and return strong type object.
    /// </summary>
    public class EventReader : IEventReader
    {
        /// <summary>
        /// Using Newtonsoft.json to deserialize the json file into List events, add error
        /// error message into error list and return strong type object.
        /// </summary>
        /// <param name="filePath">Used to read json file</param>
        /// <returns>Returns EventReaderResult</returns>
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