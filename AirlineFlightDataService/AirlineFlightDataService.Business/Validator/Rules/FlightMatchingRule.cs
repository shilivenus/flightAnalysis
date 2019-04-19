using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    /// <summary>
    /// Implement IRule interface. Matching the event based on
    /// flight field is null or not.
    /// </summary>
    public class FlightMatchingRule : IRule
    {
        /// <summary>
        /// Validate the event. Return true of false based on
        /// the flight field is null or not.
        /// </summary>
        /// <param name="flightEvent">Used to be validate</param>
        /// <returns>returns bool</returns>
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Flight != null;
        }
    }
}
