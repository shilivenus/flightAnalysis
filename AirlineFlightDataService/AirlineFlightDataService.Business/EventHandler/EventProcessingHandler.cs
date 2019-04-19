using System;
using System.IO;
using System.Linq;
using AirlineFlightDataService.Business.Enum;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Module;
using AirlineFlightDataService.Business.Validator;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AirlineFlightDataService.Business.EventHandler
{
    /// <summary>
    /// Event handler to process the events from reader result. Using validator to valudate
    /// each event. Based on validation result, writing new file to different folder, and log
    /// infomation along the way. Return event details object in the end.
    /// </summary>
    public class EventProcessingHandler : IEventProcessingHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IValidator _validator;
        private readonly ILogger _logger;

        public EventProcessingHandler(IConfiguration configuration, IValidator validator, ILogger logger)
        {
            _configuration = configuration;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Create new json file, using validator to validate each event. Based on
        /// the result of validation to create new json file in different folder or 
        /// log error message to console. Returning processed eventDetails object.
        /// </summary>
        /// <param name="eventDetails">Used to fill in event details</param>
        /// <param name="result">Used to get each event</param>
        /// <returns>Returns EventDetails object</returns>
        public EventDetails ProcessingEvent(EventDetails eventDetails, EventReaderResult result)
        {
            foreach (var flightEvent in result.Events)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                string flightEventTypeName = flightEvent.EventType.ToLower();

                //GetSection Never return null, If no matching sub-section is found with the specified key, an empty IConfigurationSection will be returned.
                var configSection = _configuration.GetSection(flightEventTypeName).GetChildren();

                var configSectionList = configSection.ToList();

                //Check configSection is empty or not
                if (configSectionList.Any())
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

                    // Count of events per type.
                    if (!eventDetails.EventDetailsList.TryAdd(flightEventTypeName, 1))
                    {
                        var count = ++eventDetails.EventDetailsList[flightEventTypeName];
                        eventDetails.EventDetailsList[flightEventTypeName] = count;
                    }

                    var arrivalEventJson = JsonConvert.SerializeObject(flightEvent);

                    // Validate each event
                    if (_validator.IsValidate(flightEvent))
                    {
                        if (!String.IsNullOrEmpty(curatedFolder))
                        {
                            CreateFileHelper(arrivalEventJson, curatedFolder, timeStamp,
                                flightEventTypeName);
                        }
                        else
                        {
                            _logger.LogInfoToConsole($"{flightEventTypeName} has no curated folder setup in appsettings.json.");
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
                            _logger.LogInfoToConsole($"{flightEventTypeName} has no exception folder setup in appsettings.json.");
                        }
                    }
                }
                else
                {
                    _logger.LogInfoToConsole($"{flightEventTypeName} has no folder path setup in appsettings.json");
                }
            }

            return eventDetails;
        }

        /// <summary>
        /// A helper to write the file into folder.
        /// </summary>
        /// <param name="file">File will be writen into destination folder</param>
        /// <param name="filePath">File path of the original file</param>
        /// <param name="timeStamp">Used to naming new file</param>
        /// <param name="eventType">Used to naming new file</param>
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
