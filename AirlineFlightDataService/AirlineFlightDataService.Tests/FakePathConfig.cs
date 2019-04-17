using AirlineFlightDataService.Configuration;

namespace AirlineFlightDataService.Tests
{
    class FakePathConfig : IFilePathConfiguration
    {
        public string DestinationFileFolder { get; set; }
        public string SourceFileFolder { get; set; }
        public string ExceptionFileFolder { get; set; }
        public string ExceptionArrivalFilePath { get; set; }
        public string ExceptionDepartureFilePath { get; set; }
        public string CuratedArrivalFilePath { get; set; }
        public string CuratedDepartureFilePath { get; set; }
    }
}
