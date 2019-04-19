using System.IO;

namespace AirlineFlightDataService.Business.EventHandler
{
    /// <summary>
    /// Represents an event handler for watcher.
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// A delegate method for watcher to handler a file
        /// creation event.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        void OnCreated(object source, FileSystemEventArgs e);
    }
}