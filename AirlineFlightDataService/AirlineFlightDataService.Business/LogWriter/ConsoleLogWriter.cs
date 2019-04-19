using System;

namespace AirlineFlightDataService.Business.LogWriter
{
    /// <summary>
    /// NLog wrapper. 
    /// </summary>
    public class ConsoleLogWriter : ILogWriter
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Using NLog to log information message to console.
        /// </summary>
        /// <param name="infoMessage"></param>
        public void WriteInfoToConsole(string infoMessage)
        {
            _logger.Info(infoMessage);
        }

        /// <summary>
        /// Using Nlog to log error details to console.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="errorMessage"></param>
        public void WriteErrorToConsole(Exception e, string errorMessage)
        {
            _logger.Error(e, errorMessage);
        }
    }
}
