using System;
using System.Collections.Generic;
using System.Threading;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using AirlineFlightDataService.Validator.Rules;
using Ninject;

namespace AirlineFlightDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new bindings());

            EventProcessor processor = kernel.Get<EventProcessor>();
            PathConfiguration config = new PathConfiguration();

            Watcher watcher = new Watcher(processor, config);

            watcher.Run();
        }
    }
}
