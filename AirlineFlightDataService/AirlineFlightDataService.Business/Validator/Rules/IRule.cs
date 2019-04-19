using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    /// <summary>
    /// Represents a rule.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Matching the event with the rule.
        /// </summary>
        /// <param name="flightEvent">Used to be validate</param>
        /// <returns>Returns bool</returns>
        bool IsMatched(Event flightEvent);
    }
}