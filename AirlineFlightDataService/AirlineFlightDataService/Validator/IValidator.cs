using AirlineFlightDataService.Module;

namespace AirlineFlightDataService.ValidationRule
{
    public interface IValidator
    {
        bool IsValidate(Event flightEvent);
    }
}