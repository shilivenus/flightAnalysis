using System.IO;

namespace AirlineFlightDataService.EventHandler
{
    public interface IEventHandler
    {
        void OnCreated(object source, FileSystemEventArgs e);
    }
}