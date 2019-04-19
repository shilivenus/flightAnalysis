using System;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.LogWriter;
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
                var kernel = new StandardKernel(new Bindings());

                IWatcher watcher = kernel.Get<IWatcher>();

                watcher.Run();
            }
            catch (Exception e)
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Error(e,e.Message);
            }
        }
    }
}
