using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.Configuration;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Watcher;
using AutoFixture;
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
            var fixture = new Fixture();

            var fakePathConfig = fixture.Build<FakePathConfig>().Create();

            var mockEventHandler = new Mock<IEventHandler>();            

            var watcher = new FlightWatcher(fakePathConfig, mockEventHandler.Object);

            //Assert
            Assert.Throws<Exception>(() => watcher.Run());
        }
    }
}
