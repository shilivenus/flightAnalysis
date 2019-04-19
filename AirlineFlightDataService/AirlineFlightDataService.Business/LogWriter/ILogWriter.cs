using System;

namespace AirlineFlightDataService.Business.LogWriter
{
    public interface ILogWriter
    {
        void WriteInfoToConsole(string infoMessage);
        void WriteErrorToConsole(Exception e, string errorMessage);
    }
}