using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator.Rules
{
    class FlightMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Flight != null;
        }
    }
}
