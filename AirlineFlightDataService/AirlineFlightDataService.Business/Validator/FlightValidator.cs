using System.Collections.Generic;
using AirlineFlightDataService.Business.Module;
using AirlineFlightDataService.Business.Validator.Rules;

namespace AirlineFlightDataService.Business.Validator
{
    public class FlightValidator : IValidator
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
