using System;
using System.IO;
using AirlineFlightDataService.EventHandler;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService.Watcher
{
    public class FlightWatcher : IWatcher
    {
        private readonly IEventHandler _eventHandler;
        private readonly IConfiguration _configuration;

        public FlightWatcher(IEventHandler eventHandler, IConfiguration configuration)
        {
            _eventHandler = eventHandler;
            _configuration = configuration;
        }

        public void Run()
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    var sourceFileFolder = _configuration["source"];

                    if (!Directory.Exists(sourceFileFolder))
                        throw new Exception($"{sourceFileFolder} does not exist.");

                    watcher.Path = sourceFileFolder;

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

