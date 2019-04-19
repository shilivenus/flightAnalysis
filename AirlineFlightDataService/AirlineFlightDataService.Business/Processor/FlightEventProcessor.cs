using System;
using System.Collections.Generic;
using System.Diagnostics;
using AirlineFlightDataService.Business.EventHandler;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Module;
using AirlineFlightDataService.Business.Reader;

namespace AirlineFlightDataService.Business.Processor
{
    /// <summary>
    /// Flight Event Processor. Reading file, using different handler to process event,
    /// and log event infomation in the end.
    /// </summary>
    public class FlightEventProcessor : IEventProcessor
    {
        private readonly ILogger _logger;
        private readonly IEventReader _eventReader;
        private readonly IErrorsProcessingHandler _errorsProcessingHandler;
        private readonly IEventProcessingHandler _eventProcessingHandler;

        public FlightEventProcessor(ILogger logger, IEventReader eventReader, IErrorsProcessingHandler errorsProcessingHandler, IEventProcessingHandler eventProcessingHandler)
        {
            _logger = logger;
            _eventReader = eventReader;
            _errorsProcessingHandler = errorsProcessingHandler;
            _eventProcessingHandler = eventProcessingHandler;
        }

        /// <summary>
        /// Read the file. Send file to different handlers and log the event details.
        /// </summary>
        /// <param name="filePath">File full path include name</param>
        /// <param name="fileName">File Name</param>
        public void Process(string filePath, string fileName)
        {
            EventDetails eventDetails = new EventDetails(new Dictionary<string, int>(), new List<string>(), 0);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = _eventReader.Read(filePath);

            if(result?.Errors != null && result.Errors.Count != 0)
            {
                _errorsProcessingHandler.ProcessingErrors(filePath, fileName, result);
            }
                
            if(result?.Events != null)
            {
                eventDetails = _eventProcessingHandler.ProcessingEvent(eventDetails, result);
            }
            else
            {
                _logger.LogInfoToConsole($"{filePath} cannot be converted to json");
            }            

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            _logger.LogEventInfo(eventDetails, ts);
        }
    }
}
