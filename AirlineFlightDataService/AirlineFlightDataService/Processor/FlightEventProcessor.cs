using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.Enum;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Processor
{
    public class FlightEventProcessor : IEventProcessor
    {
        private readonly IValidator _validator;
        private readonly ILogger _logger;
        private readonly IEventReader _eventReader;

        public FlightEventProcessor(IValidator validator, ILogger logger, IEventReader eventReader)
        {
            _validator = validator;
            _logger = logger;
            _eventReader = eventReader;
        }

        public void Process(string filePath, string fileName, IFilePathConfiguration pathConfiguration)
        {
            EventDetails eventDetails = new EventDetails(new Dictionary<string, int>(), new List<string>(), 0);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = _eventReader.Read(filePath);

            if(result?.Errors != null && result.Errors.Count != 0)
            {
                ErrorsProcessingHandler(filePath, fileName, pathConfiguration, result);
            }
                
            if(result?.Events != null)
            {
                eventDetails = EventProcessingHandler(pathConfiguration, eventDetails, result);
            }
            else
            {
                Console.WriteLine($"{filePath} cannot be converted to json");
            }            

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            _logger.LogEventInfo(eventDetails, ts);
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

        private void ErrorsProcessingHandler(string filePath, string fileName, IFilePathConfiguration pathConfiguration, EventReaderResult result)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"{filePath} meet following errors {error}");
            }

            if (!Directory.Exists(pathConfiguration.ExceptionFileFolder))
            {
                throw new Exception($"{pathConfiguration.ExceptionFileFolder} does not exist.");
            }

            var exceptionFilePath = Path.Combine(pathConfiguration.ExceptionFileFolder, fileName);

            if (File.Exists(exceptionFilePath))
            {
                throw new Exception($"{exceptionFilePath} has existed.");
            }

            File.Copy(filePath, exceptionFilePath);
        }

        private EventDetails EventProcessingHandler(IFilePathConfiguration pathConfiguration, EventDetails eventDetails,
            EventReaderResult result)
        {
            foreach (var flightEvent in result.Events)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");

                string arrivalEventTypeName = EventType.Arrival.ToString();
                string departureEventTypeName = EventType.Departure.ToString();

                switch (flightEvent.EventType)
                {
                    case EventType.Arrival:
                        if (!eventDetails.EventDetailsList.TryAdd(arrivalEventTypeName, 1))
                        {
                            var count = ++eventDetails.EventDetailsList[arrivalEventTypeName];
                            eventDetails.EventDetailsList[arrivalEventTypeName] = count;
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
                            eventDetails.FailedEventList.Add($"{arrivalEventTypeName}-{timeStamp}");
                            eventDetails.FailedEventCount++;
                        }
                        break;
                    case EventType.Departure:
                        if (!eventDetails.EventDetailsList.TryAdd(departureEventTypeName, 1))
                        {
                            var count = ++eventDetails.EventDetailsList[departureEventTypeName];
                            eventDetails.EventDetailsList[departureEventTypeName] = count;
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
                            eventDetails.FailedEventList.Add($"{departureEventTypeName}-{timeStamp}");
                            eventDetails.FailedEventCount++;
                        }
                        break;
                    default:
                        throw new Exception($"{flightEvent.EventType} cannot be processed.");
                }
            }

            return eventDetails;
        }
    }
}
