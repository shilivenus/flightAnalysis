using System;
using System.IO;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;

namespace AirlineFlightDataService.Watcher
{
    public class FlightWatcher : IWatcher
    {
        private readonly PathConfiguration _pathConfiguration;
        private readonly IEventProcessor _eventProcessor;

        public FlightWatcher(IEventProcessor eventProcessor, PathConfiguration pathConfiguration)
        {
            _eventProcessor = eventProcessor;
            _pathConfiguration = pathConfiguration;
        }

        public void Run()
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    if (!Directory.Exists(_pathConfiguration._source))
                        throw new Exception($"{_pathConfiguration._source} does not exist.");

                    watcher.Path = _pathConfiguration._source;

                    // Watch for changes in LastAccess and LastWrite times, and
                    // the renaming of files or directories.
                    watcher.NotifyFilter = NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                    // Only watch text files.
                    watcher.Filter = "*.json";

                    // Add event handlers.
                    watcher.Created += OnCreated;

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

        // Define the event handlers.
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            // Check file existence before sent to process.
            if (!File.Exists(e.FullPath))
                throw new Exception("There is no file been created.");

            // Specify what is done when a file is created.
            Console.WriteLine($"File: {e.FullPath}");

            _eventProcessor.Process(e.FullPath, _pathConfiguration);

            var destinationFilePath = Path.Combine(_pathConfiguration._destination, e.Name);

            if (File.Exists(destinationFilePath))
            {
                throw new Exception($"{destinationFilePath} has been processed before.");
            }

            File.Copy(Path.Combine(_pathConfiguration._source, e.Name), destinationFilePath);
        }
    }
}

