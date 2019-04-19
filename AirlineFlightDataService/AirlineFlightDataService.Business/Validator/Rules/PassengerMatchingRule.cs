using AirlineFlightDataService.Business.Module;

namespace AirlineFlightDataService.Business.Validator.Rules
{
    /// <summary>
    /// Implement IRule interface. Matching the event based on
    /// Passengers field is large than zero or not.
    /// </summary>
    public class PassengerMatchingRule : IRule
    {
        /// <summary>
        /// Validate the event. Return true of false based on
        /// the Passengers field is large than zero or not.
        /// </summary>
        /// <param name="flightEvent">Used to be validate</param>
        /// <returns>Returns bool</returns>
        public bool IsMatched(Event flightEvent)
        {
            return flightEvent.Passengers > 0;
        }
    }
}
