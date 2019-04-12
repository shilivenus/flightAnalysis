using System;
using System.Collections.Generic;
using System.Threading;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator.Rules;

namespace AirlineFlightDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            EventReader reader = new EventReader(new EventLogger());
            Validator.Validator validator = new Validator.Validator(new List<IRule>{new FlightMatchingRule(), new PassengerMatchingRule()});
            EventProcessor processor = new EventProcessor(validator);
            PathConfiguration config = new PathConfiguration();

            Watcher watcher = new Watcher(reader, processor, config);

            watcher.Run();
        }
    }
}
