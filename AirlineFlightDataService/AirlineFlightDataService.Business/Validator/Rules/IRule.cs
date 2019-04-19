using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    public interface IRule
    {
        bool IsMatched(Event flightEvent);
    }
}