using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AirlineFlightDataService.Enum;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.ValidationRule;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Processor
{
    class EventProcessor : IEventProcessor
    {
        private readonly IValidator _validator;

        public EventProcessor(IValidator validator)
        {
            _validator = validator;
        }

        public void Process(List<Event> events, PathConfiguration pathConfiguration)
        {
            foreach (var flightEvent in events)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                switch (flightEvent.EventType)
                {
                    case EventType.Arrival:
                        var arrivalEventJson = JsonConvert.SerializeObject(flightEvent);
                        CreateFileHelper(arrivalEventJson,
                            _validator.IsValidate(flightEvent)
                                ? pathConfiguration._curatedArrivalFilePath
                                : pathConfiguration._exceptionArrivalFilePath, timeStamp, EventType.Arrival.ToString());
                        break;
                    case EventType.Departure:
                        var departureJson = JsonConvert.SerializeObject(flightEvent);
                        CreateFileHelper(departureJson,
                            _validator.IsValidate(flightEvent)
                                ? pathConfiguration._curatedDepartureFilePath
                                : pathConfiguration._exceptionDepartureFilePath, timeStamp,
                            EventType.Departure.ToString());
                        break;
                }
            }
        }

        private void CreateFileHelper(string file, string filePath, string timeStamp, string eventType)
        {
            var destination = Path.Combine(filePath, $"{eventType}-{timeStamp}");
            File.WriteAllText(destination, file);
        }
    }
}
