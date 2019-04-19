using System;
using System.IO;
using AirlineFlightDataService.Business.Reader;
using Xunit;

namespace AirlineFlightDataService.Tests.Reader
{
    public class EventReaderTest
    {
        [Fact]
        public void ReturnErrosWithInvalidJson()
        {
            //Arrange
            var basePath = filePathHelper();
            var fileLocation = "TestJsonFile\\exception.json";

            var filePath = Path.Combine(basePath, fileLocation);

            var reader = new EventReader();

            //Act
            var result = reader.Read(filePath);

            //Assert
            Assert.Null(result.Events);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void ReturnEventsWithValidJson()
        {
            //Arrange
            var basePath = filePathHelper();
            var fileLocation = "TestJsonFile\\departure.json";

            var filePath = Path.Combine(basePath, fileLocation);

            var reader = new EventReader();

            var expect = "Departure";

            //Act
            var result = reader.Read(filePath);

            //Assert
            Assert.Empty(result.Errors);
            Assert.NotEmpty(result.Events);
            Assert.Equal(expect, result.Events[0].EventType.ToString());
        }

        private string filePathHelper()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            int position = baseDirectory.IndexOf("bin", StringComparison.Ordinal);

            return baseDirectory.Substring(0, position - 0);
        }
    }
}
