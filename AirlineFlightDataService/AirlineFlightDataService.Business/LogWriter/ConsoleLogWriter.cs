using System;

namespace AirlineFlightDataService.Business.LogWriter
{
    public class ConsoleLogWriter : ILogWriter
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void WriteInfoToConsole(string infoMessage)
        {
            _logger.Info(infoMessage);
        }

        public void WriteErrorToConsole(Exception e, string errorMessage)
        {
            _logger.Error(e, errorMessage);
        }
    }
}
