using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    public class FlightMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Flight != null;
        }
    }
}
