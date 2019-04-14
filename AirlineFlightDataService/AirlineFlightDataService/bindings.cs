using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using AirlineFlightDataService.Validator.Rules;
using AirlineFlightDataService.Watcher;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineFlightDataService
{
    public class bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator>().To<FlightValidator>();
            Bind<IEventReader>().To<EventReader>();
            Bind<IEventProcessor>().To<EventProcessor>();
            Bind<IRule>().To<FlightMatchingRule>();
            Bind<IRule>().To<PassengerMatchingRule>();
            Bind<ILogger>().To<EventLogger>();
            Bind<IWatcher>().To<FlightWatcher>();
        }
    }
}
