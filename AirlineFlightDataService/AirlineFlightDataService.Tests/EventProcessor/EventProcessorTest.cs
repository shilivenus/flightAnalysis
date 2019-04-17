using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AirlineFlightDataService.Validator;
using AutoFixture;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventProcessor
{
    public class EventProcessorTest
    {
        [Fact]
        public void ThrowExceptionWhenCallingCreateFileHelperWithInvalidFilePath()
        {
            //Arrange
            var fixture = new Fixture();

            var file = fixture.Build<string>().Create();
            var config = fixture.Build<FakePathConfig>().Create();
            var eventReaderResult = fixture.Build<EventReaderResult>().Create();

            var mockValidator = new Mock<IValidator>();
            var mockLogger = new Mock<ILogger>();
            var mockReader = new Mock<IEventReader>();

            mockReader.Setup(r => r.Read(file)).Returns(eventReaderResult);

            var processor = new FlightEventProcessor(mockValidator.Object, mockLogger.Object, mockReader.Object);

            //Assert
            Assert.Throws<Exception>(() => processor.Process(file, "test", config));
        }
    }
}
