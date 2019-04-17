using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Processor;
using AutoFixture;
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

            var fakePathConfig = fixture.Build<FakePathConfig>().Create();

            var e = fixture.Build<FileSystemEventArgs>().Create();

            var mockEventHandler = new Mock<IEventProcessor>();

            var eventHandler = new FlightEventHandler(mockEventHandler.Object, fakePathConfig);

            //Assert
            Assert.Throws<Exception>(() => eventHandler.OnCreated(new object(), e));
        }
    }
}
