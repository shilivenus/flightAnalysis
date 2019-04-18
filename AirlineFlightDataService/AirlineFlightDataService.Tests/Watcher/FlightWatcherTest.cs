using System;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Watcher;
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

            mockConfig.Setup(c => c["source"]).Returns("Test");

            var watcher = new FlightWatcher(mockEventHandler.Object, mockConfig.Object);

            //Assert
            Assert.Throws<Exception>(() => watcher.Run());
        }
    }
}
