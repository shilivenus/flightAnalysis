using System;
using System.IO;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Processor;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService.Business.EventHandler
{
    /// <summary>
    /// Cope the file to raw folder, and send the file to processor
    /// </summary>
    public class FlightEventHandler : IEventHandler
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public FlightEventHandler(IEventProcessor eventProcessor, IConfiguration configuration, ILogger logger)
        {
            _eventProcessor = eventProcessor;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Delegate method for the watcher, copy file to raw folder 
        /// after file and folder existence checking, and send the file
        /// to processor.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnCreated(object source, FileSystemEventArgs e)
        {
            var destinationFileFolder = _configuration["destination"];
            var sourceFileFolder = _configuration["source"];

            // Check file existence before sent to process.
            if (!File.Exists(e.FullPath))
                throw new Exception("There is no file been created.");

            // Specify what is done when a file is created.
            _logger.LogInfoToConsole($"File: {e.FullPath}");

            if (!Directory.Exists(destinationFileFolder))
            {
                throw new Exception($"{destinationFileFolder} does not exist.");
            }

            var destinationFilePath = Path.Combine(destinationFileFolder, e.Name);

            if (File.Exists(destinationFilePath))
            {
                throw new Exception($"{destinationFilePath} has been processed before.");
            }

            File.Copy(Path.Combine(sourceFileFolder, e.Name), destinationFilePath);

            _eventProcessor.Process(e.FullPath, e.Name);
        }
    }
}
