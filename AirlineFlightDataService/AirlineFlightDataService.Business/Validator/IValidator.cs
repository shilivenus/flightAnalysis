using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator
{
    /// <summary>
    /// Represents a validator.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validate the event
        /// </summary>
        /// <param name="flightEvent">Used to be validate</param>
        /// <returns>Returns bool</returns>
        bool IsValidate(Event flightEvent);
    }
}