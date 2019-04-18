using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using AirlineFlightDataService.Validator.Rules;
using AirlineFlightDataService.Watcher;
using Ninject.Modules;
using System.IO;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.LogWriter;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Bind<IConfiguration>().ToConstant(configuration);
            Bind<IValidator>().To<FlightValidator>();
            Bind<IEventReader>().To<EventReader>();
            Bind<IEventProcessor>().To<FlightEventProcessor>();
            Bind<IRule>().To<FlightMatchingRule>();
            Bind<IRule>().To<PassengerMatchingRule>();
            Bind<ILogger>().To<FlightEventLogger>();
            Bind<IWatcher>().To<FlightWatcher>();
            Bind<IEventHandler>().To<FlightEventHandler>();
            Bind<IEventProcessingHandler>().To<EventProcessingHandler>();
            Bind<IErrorsProcessingHandler>().To<ErrorsProcessingHandler>();
            Bind<ILogWriter>().To<ConsoleLogWriter>();
        }
    }
}
