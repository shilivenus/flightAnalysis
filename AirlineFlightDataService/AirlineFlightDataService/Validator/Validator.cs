using System;
using System.Collections.Generic;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.ValidationRule;
using AirlineFlightDataService.Validator.Rules;

namespace AirlineFlightDataService.Validator
{
    class Validator : IValidator
    {
        private readonly IEnumerable<IRule> _rules;

        public Validator(IEnumerable<IRule> rules)
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
