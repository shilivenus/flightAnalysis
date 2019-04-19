using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    public class PassengerMatchingRule : IRule
    {
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Passengers > 0;
        }
    }
}
