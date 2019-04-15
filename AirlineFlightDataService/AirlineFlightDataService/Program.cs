using System;
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
                Console.WriteLine(e.Message);
            }
        }
    }
}
