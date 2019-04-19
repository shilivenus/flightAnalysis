using System.IO;
using AirlineFlightDataService.Business.EventHandler;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.LogWriter;
using AirlineFlightDataService.Business.Processor;
using AirlineFlightDataService.Business.Reader;
using AirlineFlightDataService.Business.Validator;
using AirlineFlightDataService.Business.Validator.Rules;
using AirlineFlightDataService.Business.Watcher;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;

namespace AirlineFlightDataService.Business
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
