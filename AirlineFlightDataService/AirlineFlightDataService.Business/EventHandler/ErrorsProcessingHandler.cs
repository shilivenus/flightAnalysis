using System;
using System.IO;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Module;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService.Business.EventHandler
{
    public class ErrorsProcessingHandler : IErrorsProcessingHandler
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ErrorsProcessingHandler(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void ProcessingErrors(string filePath, string fileName, EventReaderResult result)
        {
            var exceptionFileFolder = _configuration["exceptionFolder"];

            foreach (var error in result.Errors)
            {
                _logger.LogInfoToConsole($"{filePath} meet following errors: {error}");
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
