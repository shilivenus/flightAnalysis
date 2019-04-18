using System;
using System.IO;
using AirlineFlightDataService.Enum;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Validator;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AirlineFlightDataService.EventHandler
{
    public class EventProcessingHandler : IEventProcessingHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IValidator _validator;

        public EventProcessingHandler(IConfiguration configuration, IValidator validator)
        {
            _configuration = configuration;
            _validator = validator;
        }

        public EventDetails ProcessingEvent(EventDetails eventDetails, EventReaderResult result)
        {
            foreach (var flightEvent in result.Events)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                string flightEventTypeName = flightEvent.EventType.ToLower();

                var configSection = _configuration.GetSection(flightEventTypeName).GetChildren();

                if (configSection != null)
                {
                    string curatedFolder = null;
                    string exceptionFolder = null;
                    foreach (var config in configSection)
                    {
                        if (config.Key == FolderType.Curated.ToString().ToLower())
                        {
                            curatedFolder = config.Value;
                        }

                        if (config.Key == FolderType.Exception.ToString().ToLower())
                        {
                            exceptionFolder = config.Value;
                        }
                    }

                    if (!eventDetails.EventDetailsList.TryAdd(flightEventTypeName, 1))
                    {
                        var count = ++eventDetails.EventDetailsList[flightEventTypeName];
                        eventDetails.EventDetailsList[flightEventTypeName] = count;
                    }

                    var arrivalEventJson = JsonConvert.SerializeObject(flightEvent);

                    if (_validator.IsValidate(flightEvent))
                    {
                        if (!String.IsNullOrEmpty(curatedFolder))
                        {
                            CreateFileHelper(arrivalEventJson, curatedFolder, timeStamp,
                                flightEventTypeName);
                        }
                        else
                        {
                            Console.WriteLine($"{flightEventTypeName} has no curated folder setup in appsettings.json.");
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(exceptionFolder))
                        {
                            CreateFileHelper(arrivalEventJson, exceptionFolder, timeStamp,
                                flightEventTypeName);
                            eventDetails.FailedEventList.Add($"{flightEventTypeName}-{timeStamp}");
                            eventDetails.FailedEventCount++;
                        }
                        else
                        {
                            Console.WriteLine($"{flightEventTypeName} has no exception folder setup in appsettings.json.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{flightEventTypeName} has no folder path setup in appsettings.json");
                }
            }

            return eventDetails;
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
