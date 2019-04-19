using System;
using AirlineFlightDataService.Business.EventHandler;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Watcher;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.Watcher
{
    public class FlightWatcherTest
    {
        [Fact]
        public void ThrowExceptionWhenFolderDoseNotExist()
        {
            //Arrange
            var mockEventHandler = new Mock<IEventHandler>();
            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();

            mockConfig.Setup(c => c["source"]).Returns("Test");

            var watcher = new FlightWatcher(mockEventHandler.Object, mockConfig.Object, mockLogger.Object);

            //Assert
            Assert.Throws<Exception>(() => watcher.Run());
        }
    }
}
