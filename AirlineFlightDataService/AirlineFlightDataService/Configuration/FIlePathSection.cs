using System.Configuration;

namespace AirlineFlightDataService.Configuration
{
    internal class FilePathSection : ConfigurationSection, IFilePathConfiguration
    {
        [ConfigurationProperty("destinationFileFolder", IsRequired = true)]
        public string DestinationFileFolder => (string)this["destinationFileFolder"];

        [ConfigurationProperty("sourceFileFolder", IsRequired = true)]
        public string SourceFileFolder => (string)this["sourceFileFolder"];

        [ConfigurationProperty("exceptionFileFolder", IsRequired = true)]
        public string ExceptionFileFolder => (string)this["exceptionFileFolder"];

        [ConfigurationProperty("exceptionArrivalFilePath", IsRequired = true)]
        public string ExceptionArrivalFilePath => (string)this["exceptionArrivalFilePath"];

        [ConfigurationProperty("exceptionDepartureFilePath", IsRequired = true)]
        public string ExceptionDepartureFilePath => (string)this["exceptionDepartureFilePath"];

        [ConfigurationProperty("curatedArrivalFilePath", IsRequired = true)]
        public string CuratedArrivalFilePath => (string)this["curatedArrivalFilePath"];

        [ConfigurationProperty("curatedDepartureFilePath", IsRequired = true)]
        public string CuratedDepartureFilePath => (string)this["curatedDepartureFilePath"];
    }
}
