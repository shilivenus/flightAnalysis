using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator.Rules
{
    public class FlightMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Flight != null;
        }
    }
}
