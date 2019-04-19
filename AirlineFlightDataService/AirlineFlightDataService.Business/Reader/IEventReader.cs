using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Reader
{
    /// <summary>
    /// Represents an event reader.
    /// </summary>
    public interface IEventReader
    {
        /// <summary>
        /// Read file from file path and return EventReaderResult.
        /// </summary>
        /// <param name="filePath">Used to read file</param>
        /// <returns>Returns EventReaderResult</returns>
        EventReaderResult Read(string filePath);
    }
}