using System;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Watcher;
using Ninject;

namespace AirlineFlightDataService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var kernel = new StandardKernel(new bindings());

                IEventProcessor processor = kernel.Get<IEventProcessor>();
                PathConfiguration config = new PathConfiguration();

                IWatcher watcher = new FlightWatcher(processor, config);

                watcher.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
