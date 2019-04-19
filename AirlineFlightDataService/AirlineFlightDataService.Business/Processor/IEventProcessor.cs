namespace AirlineFlightDataService.Business.Processor
{
    /// <summary>
    /// Represents an event processor.
    /// </summary>
    public interface IEventProcessor
    {
        /// <summary>
        /// Process file.
        /// </summary>
        /// <param name="filePath">File full path</param>
        /// <param name="fileName">File name</param>
        void Process(string filePath, string fileName);
    }
}