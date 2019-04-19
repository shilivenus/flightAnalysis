using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator
{
    public interface IValidator
    {
        bool IsValidate(Event flightEvent);
    }
}