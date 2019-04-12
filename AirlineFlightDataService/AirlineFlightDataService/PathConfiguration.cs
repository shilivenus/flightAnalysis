using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineFlightDataService
{
    public class PathConfiguration
    {
        public string _destination = @"C:\ProcessorRoot\02-RAW";
        public string _source = @"C:\ProcessorRoot\01-Input";
        public string _exceptionArrivalFilePath = @"C:\ProcessorRoot\03-Exception\Arrival";
        public string _exceptionDepartureFilePath = @"C:\ProcessorRoot\03-Exception\Departure";
        public string _curatedArrivalFilePath = @"C:\ProcessorRoot\04-Curated\Arrival";
        public string _curatedDepartureFilePath = @"C:\ProcessorRoot\04-Curated\Departure";
    }
}
