using System.IO;

namespace AirlineFlightDataService.Business.EventHandler
{
    public interface IEventHandler
    {
        void OnCreated(object source, FileSystemEventArgs e);
    }
}