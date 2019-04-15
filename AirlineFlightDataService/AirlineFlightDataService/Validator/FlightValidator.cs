using System.Collections.Generic;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Validator.Rules;

namespace AirlineFlightDataService.Validator
{
    class FlightValidator : IValidator
    {
        private readonly IEnumerable<IRule> _rules;

        public FlightValidator(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

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
