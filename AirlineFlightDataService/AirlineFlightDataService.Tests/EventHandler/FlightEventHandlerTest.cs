using System;
using System.IO;
using AirlineFlightDataService.Business.EventHandler;
using AirlineFlightDataService.Business.Logger;
using AirlineFlightDataService.Business.Processor;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventHandler
{
    public class FlightEventHandlerTest
    {
        [Fact]
        public void ThorwExceptionWhen()
        {
            //Arrange
            var fixture = new Fixture();

            var e = fixture.Build<FileSystemEventArgs>().Create();

            var mockEventHandler = new Mock<IEventProcessor>();
            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();

            mockConfig.Setup(c => c["source"]).Returns("Test");
            mockConfig.Setup(c => c["destination"]).Returns("Test");

            var eventHandler = new FlightEventHandler(mockEventHandler.Object, mockConfig.Object, mockLogger.Object);

            //Assert
            Assert.Throws<Exception>(() => eventHandler.OnCreated(new object(), e));
        }
    }
}
