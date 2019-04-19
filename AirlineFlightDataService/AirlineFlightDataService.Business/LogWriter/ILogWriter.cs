using System;

namespace AirlineFlightDataService.Business.LogWriter
{
    /// <summary>
    /// Represents a logWriter
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Log information to console.
        /// </summary>
        /// <param name="infoMessage">Used to be logged to console</param>
        void WriteInfoToConsole(string infoMessage);

        /// <summary>
        /// Log error details to console.
        /// </summary>
        /// <param name="e">Exception to be logged</param>
        /// <param name="errorMessage">Excpetion message to be logged</param>
        void WriteErrorToConsole(Exception e, string errorMessage);
    }
}