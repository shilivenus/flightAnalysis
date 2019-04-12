using System;
using System.IO;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;

namespace AirlineFlightDataService
{
    public class Watcher
    {
        private readonly PathConfiguration _pathConfiguration;
        private readonly IEventReader _eventReader;
        private readonly IEventProcessor _eventProcessor;

        public Watcher(IEventReader eventReader, IEventProcessor eventProcessor, PathConfiguration pathConfiguration)
        {
            _eventReader = eventReader;
            _eventProcessor = eventProcessor;
            _pathConfiguration = pathConfiguration;
        }

        public void Run()
        {

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
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
                watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e) =>
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

        private void OnRenamed(object source, RenamedEventArgs e) =>
            // Specify what is done when a file is renamed.
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine($"File: {e.FullPath}");

            var result = _eventReader.Read(e.FullPath);

            _eventProcessor.Process(result, _pathConfiguration);

            File.Copy(Path.Combine(_pathConfiguration._source, e.Name), Path.Combine(_pathConfiguration._destination, e.Name));
        }
    }
}

