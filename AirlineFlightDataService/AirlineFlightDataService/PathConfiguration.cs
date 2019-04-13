using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AirlineFlightDataService
{
    public class PathConfiguration
    {
        public string _destination = ConfigurationManager.AppSettings["DestinationFileFolder"];
        public string _source = ConfigurationManager.AppSettings["SourceFileFolder"];
        public string _exceptionArrivalFilePath = ConfigurationManager.AppSettings["ExceptionArrivalFilePath"];
        public string _exceptionDepartureFilePath = ConfigurationManager.AppSettings["ExceptionDepartureFilePath"];
        public string _curatedArrivalFilePath = ConfigurationManager.AppSettings["CuratedArrivalFilePath"];
        public string _curatedDepartureFilePath = ConfigurationManager.AppSettings["CuratedDepartureFilePath"];
    }
}
