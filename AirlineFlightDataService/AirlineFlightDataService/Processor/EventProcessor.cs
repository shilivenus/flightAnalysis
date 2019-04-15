using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.Enum;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Processor
{
    class EventProcessor : IEventProcessor
    {
        private readonly IValidator _validator;
        private readonly ILogger _logger;
        private readonly IEventReader _eventReader;

        public EventProcessor(IValidator validator, ILogger logger, IEventReader eventReader)
        {
            _validator = validator;
            _logger = logger;
            _eventReader = eventReader;
        }

        public void Process(string filePath, IFilePathConfiguration pathConfiguration)
        {
            Dictionary<string, int> eventDetailsList = new Dictionary<string, int>();
            List<string> failedEventList = new List<string>();
            int failedEventCount = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<string> errors = new List<string>();

            var events = _eventReader.Read(filePath, errors);

            if(errors != null && errors.Count != 0)
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"{filePath} meet following errors {error}");
                }
            }
                
            if(events != null)
            {
                foreach (var flightEvent in events)
                {
                    string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");

                    string arrivalEventTypeName = EventType.Arrival.ToString();
                    string departureEventTypeName = EventType.Departure.ToString();

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
                                CreateFileHelper(arrivalEventJson, pathConfiguration.CuratedArrivalFilePath, timeStamp,
                                    arrivalEventTypeName);
                            }
                            else
                            {
                                CreateFileHelper(arrivalEventJson, pathConfiguration.ExceptionArrivalFilePath, timeStamp,
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
                                CreateFileHelper(departureJson, pathConfiguration.CuratedDepartureFilePath, timeStamp,
                                    departureEventTypeName);
                            }
                            else
                            {
                                CreateFileHelper(departureJson, pathConfiguration.ExceptionDepartureFilePath, timeStamp,
                                    departureEventTypeName);
                                failedEventList.Add($"{departureEventTypeName}-{timeStamp}");
                                failedEventCount++;
                            }
                            break;
                        default:
                            throw new Exception($"{flightEvent.EventType} cannot be processed.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"{filePath} cannot be converted to json");
            }            

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            _logger.LogEventInfo(eventDetailsList, ts, failedEventList, failedEventCount);
        }

        private void CreateFileHelper(string file, string filePath, string timeStamp, string eventType)
        {
            if (!Directory.Exists(filePath))
                throw new Exception($"{filePath} does not exist.");

            if (file == null)
                throw new Exception("There is no file been created.");

            var destination = Path.Combine(filePath, $"{eventType}-{timeStamp}");
            File.WriteAllText(destination, file);
        }
    }
}
