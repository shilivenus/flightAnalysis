using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator.Rules
{
    public interface IRule
    {
        bool IsMatched(Event flightEvent);
    }
}