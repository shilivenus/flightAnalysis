using System;
using System.IO;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.Processor;

namespace AirlineFlightDataService.EventHandler
{
    public class FlightEventHandler : IEventHandler
    {
        private readonly IFilePathConfiguration _pathConfiguration;
        private readonly IEventProcessor _eventProcessor;

        public FlightEventHandler(IEventProcessor eventProcessor, IFilePathConfiguration pathConfiguration)
        {
            _eventProcessor = eventProcessor;
            _pathConfiguration = pathConfiguration;
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            // Check file existence before sent to process.
            if (!File.Exists(e.FullPath))
                throw new Exception("There is no file been created.");

            // Specify what is done when a file is created.
            Console.WriteLine($"File: {e.FullPath}");

            if (!Directory.Exists(_pathConfiguration.DestinationFileFolder))
            {
                throw new Exception($"{_pathConfiguration.DestinationFileFolder} does not exist.");
            }

            var destinationFilePath = Path.Combine(_pathConfiguration.DestinationFileFolder, e.Name);

            if (File.Exists(destinationFilePath))
            {
                throw new Exception($"{destinationFilePath} has been processed before.");
            }

            File.Copy(Path.Combine(_pathConfiguration.SourceFileFolder, e.Name), destinationFilePath);

            _eventProcessor.Process(e.FullPath, e.Name, _pathConfiguration);
        }
    }
}
