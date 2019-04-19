using System.Collections.Generic;
using AirlineFlightDataService.Business.Module;
using AirlineFlightDataService.Business.Validator.Rules;

namespace AirlineFlightDataService.Business.Validator
{
    /// <summary>
    /// Validator engine. It is used to validate each
    /// event based on different rules.
    /// </summary>
    public class FlightValidator : IValidator
    {
        private readonly IEnumerable<IRule> _rules;

        public FlightValidator(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

        /// <summary>
        /// Validate each event based on the rule to return
        /// true of false.
        /// </summary>
        /// <param name="flightEvent">Used to be validate</param>
        /// <returns>Returns bool</returns>
        public bool IsValidate(Event flightEvent)
        {
            foreach (var rule in _rules)
            {
                if (!rule.IsMatched(flightEvent))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
