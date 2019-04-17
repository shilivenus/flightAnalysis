using System;
using System.Collections.Generic;
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

        [Fact]
        public void ValidatorWillBeCalledWhenThereIsEvent()
        {
            //Arrange
            var fixture = new Fixture();

            var file = fixture.Build<string>().Create();
            var config = fixture.Build<FakePathConfig>().Create();
            var flightEvent = fixture.Build<Event>().Create();
            var eventList = new List<Event>{ flightEvent };

            var eventReaderResult = new EventReaderResult(eventList, null);
            
            var mockValidator = new Mock<IValidator>();
            var mockLogger = new Mock<ILogger>();
            var mockReader = new Mock<IEventReader>();

            mockReader.Setup(r => r.Read(file)).Returns(eventReaderResult);
            mockValidator.Setup(v => v.IsValidate(flightEvent)).Returns(true);

            var processor = new FlightEventProcessor(mockValidator.Object, mockLogger.Object, mockReader.Object);

            //Assert
            Assert.Throws<Exception>(() => processor.Process(file, "test", config));
            mockValidator.Verify(v => v.IsValidate(flightEvent), Times.AtLeastOnce);
        }
    }
}
