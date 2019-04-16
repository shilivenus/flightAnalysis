using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator.Rules
{
    public class PassengerMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Passengers > 0;
        }
    }
}
