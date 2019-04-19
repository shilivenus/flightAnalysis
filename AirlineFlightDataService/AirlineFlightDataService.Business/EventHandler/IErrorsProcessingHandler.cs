using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.EventHandler
{
    /// <summary>
    /// Represents an Error processing handler.
    /// </summary>
    public interface IErrorsProcessingHandler
    {
        /// <summary>
        /// Process errors.
        /// </summary>
        /// <param name="filePath">Original file path</param>
        /// <param name="fileName">File name</param>
        /// <param name="result">The result from reader</param>
        void ProcessingErrors(string filePath, string fileName, EventReaderResult result);
    }
}