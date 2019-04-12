using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator.Rules
{
    class PassengerMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Passengers > 0;
        }
    }
}
