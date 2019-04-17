using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using AirlineFlightDataService.Validator.Rules;
using AirlineFlightDataService.Watcher;
using Ninject.Modules;
using System.Configuration;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.EventHandler;

namespace AirlineFlightDataService
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IFilePathConfiguration>().ToMethod(context => (FilePathSection)ConfigurationManager.GetSection("filePath"));
            Bind<IValidator>().To<FlightValidator>();
            Bind<IEventReader>().To<EventReader>();
            Bind<IEventProcessor>().To<FlightEventProcessor>();
            Bind<IRule>().To<FlightMatchingRule>();
            Bind<IRule>().To<PassengerMatchingRule>();
            Bind<ILogger>().To<EventLogger>();
            Bind<IWatcher>().To<FlightWatcher>();
            Bind<IEventHandler>().To<FlightEventHandler>();
        }
    }
}
