using System;
using System.IO;
using AirlineFlightDataService.Module;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService.EventHandler
{
    public class ErrorsProcessingHandler : IErrorsProcessingHandler
    {
        private readonly IConfiguration _configuration;

        public ErrorsProcessingHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ProcessingErrors(string filePath, string fileName, EventReaderResult result)
        {
            var exceptionFileFolder = _configuration["exceptionFolder"];

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"{filePath} meet following errors {error}");
            }

            if (!Directory.Exists(exceptionFileFolder))
            {
                throw new Exception($"{exceptionFileFolder} does not exist.");
            }

            var exceptionFilePath = Path.Combine(exceptionFileFolder, fileName);

            if (File.Exists(exceptionFilePath))
            {
                throw new Exception($"{exceptionFilePath} has existed.");
            }

            File.Copy(filePath, exceptionFilePath);
        }
    }
}
