using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.Validator
{
    public interface IValidator
    {
        bool IsValidate(Event flightEvent);
    }
}