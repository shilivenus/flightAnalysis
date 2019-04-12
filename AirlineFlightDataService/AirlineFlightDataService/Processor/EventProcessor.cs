using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var flightEvent in events)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");

                Dictionary<string, int> eventDetailsList = new Dictionary<string, int>();
                List<string> failedEventList = new List<string>();
                string arrivalEventTypeName = EventType.Arrival.ToString();
                string departureEventTypeName = EventType.Departure.ToString();

                int failedEventCount = 0;

                switch (flightEvent.EventType)
                {
                    case EventType.Arrival:
                        if (!eventDetailsList.TryAdd(arrivalEventTypeName, 1))
                        {
                            var count = eventDetailsList[arrivalEventTypeName]++;
                            eventDetailsList[arrivalEventTypeName] = count;
                        }

                        var arrivalEventJson = JsonConvert.SerializeObject(flightEvent);

                        if (_validator.IsValidate(flightEvent))
                        {
                            CreateFileHelper(arrivalEventJson, pathConfiguration._curatedArrivalFilePath, timeStamp,
                                arrivalEventTypeName);
                        }
                        else
                        {
                            CreateFileHelper(arrivalEventJson, pathConfiguration._exceptionArrivalFilePath, timeStamp,
                                arrivalEventTypeName);
                            failedEventList.Add($"{arrivalEventTypeName}-{timeStamp}");
                            failedEventCount++;
                        }
                        break;
                    case EventType.Departure:
                        if (!eventDetailsList.TryAdd(departureEventTypeName, 1))
                        {
                            var count = eventDetailsList[departureEventTypeName]++;
                            eventDetailsList[departureEventTypeName] = count;
                        }
                        var departureJson = JsonConvert.SerializeObject(flightEvent);
                        if (_validator.IsValidate(flightEvent))
                        {
                            CreateFileHelper(departureJson, pathConfiguration._curatedDepartureFilePath, timeStamp,
                                departureEventTypeName);
                        }
                        else
                        {
                            CreateFileHelper(departureJson, pathConfiguration._exceptionDepartureFilePath, timeStamp,
                                departureEventTypeName);
                            failedEventList.Add($"{departureEventTypeName}-{timeStamp}");
                            failedEventCount++;
                        }
                        break;
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
        }

        private void CreateFileHelper(string file, string filePath, string timeStamp, string eventType)
        {
            var destination = Path.Combine(filePath, $"{eventType}-{timeStamp}");
            File.WriteAllText(destination, file);
        }
    }
}
