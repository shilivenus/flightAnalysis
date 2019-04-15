namespace AirlineFlightDataService.Configuration
{
    public interface IFilePathConfiguration
    {
        string DestinationFileFolder { get; }
        string SourceFileFolder { get; }
        string ExceptionArrivalFilePath { get; }
        string ExceptionDepartureFilePath { get; }
        string CuratedArrivalFilePath { get; }
        string CuratedDepartureFilePath { get; }
    }
}