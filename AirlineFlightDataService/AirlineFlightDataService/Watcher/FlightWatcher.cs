using System;
using System.IO;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.EventHandler;

namespace AirlineFlightDataService.Watcher
{
    public class FlightWatcher : IWatcher
    {
        private readonly IFilePathConfiguration _pathConfiguration;
        private readonly IEventHandler _eventHandler;

        public FlightWatcher(IFilePathConfiguration pathConfiguration, IEventHandler eventHandler)
        {
            _pathConfiguration = pathConfiguration;
            _eventHandler = eventHandler;
        }

        public void Run()
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    if (!Directory.Exists(_pathConfiguration.SourceFileFolder))
                        throw new Exception($"{_pathConfiguration.SourceFileFolder} does not exist.");

                    watcher.Path = _pathConfiguration.SourceFileFolder;

                    // Watch for create new files.
                    watcher.NotifyFilter = NotifyFilters.FileName;

                    // Only watch json files.
                    watcher.Filter = "*.json";

                    // Add event handlers.
                    watcher.Created += _eventHandler.OnCreated;

                    // Begin watching.
                    watcher.EnableRaisingEvents = true;

                    // Wait for the user to quit the program.
                    Console.WriteLine("Press 'q' to quit the sample.");
                    while (Console.Read() != 'q') ;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}

