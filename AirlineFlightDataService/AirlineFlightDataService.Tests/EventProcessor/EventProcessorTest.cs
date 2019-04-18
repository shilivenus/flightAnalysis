using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Processor;
using AirlineFlightDataService.Reader;
using AutoFixture;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventProcessor
{
    public class EventProcessorTest
    {
        [Fact]
        public void AlwaysCallingLog()
        {
            //Arrange
            var fixture = new Fixture();

            var file = fixture.Build<string>().Create();
            var eventReaderResult = fixture.Build<EventReaderResult>().Create();
            eventReaderResult.Events = null;

            var mockLogger = new Mock<ILogger>();
            var mockReader = new Mock<IEventReader>();
            var mockErrorsProcessingHandler = new Mock<IErrorsProcessingHandler>();
            var mockEventProcessingHandler = new Mock<IEventProcessingHandler>();

            mockReader.Setup(r => r.Read(file)).Returns(eventReaderResult);

            var processor = new FlightEventProcessor(mockLogger.Object, mockReader.Object, mockErrorsProcessingHandler.Object, mockEventProcessingHandler.Object);

            //Act
            processor.Process(file, "test");

            //Assert
            mockLogger.Verify(l => l.LogToConsole($"{file} cannot be converted to json"), Times.Once);
        }

        [Fact]
        public void ErrorHandlerWillBeCalledOnceWhenErrorIsNotNull()
        {
            //Arrange
            var fixture = new Fixture();

            var file = fixture.Build<string>().Create();
            var eventReaderResult = fixture.Build<EventReaderResult>().Create();
            eventReaderResult.Events = null;

            var mockLogger = new Mock<ILogger>();
            var mockReader = new Mock<IEventReader>();
            var mockErrorsProcessingHandler = new Mock<IErrorsProcessingHandler>();
            var mockEventProcessingHandler = new Mock<IEventProcessingHandler>();

            mockReader.Setup(r => r.Read(file)).Returns(eventReaderResult);

            var processor = new FlightEventProcessor(mockLogger.Object, mockReader.Object, mockErrorsProcessingHandler.Object, mockEventProcessingHandler.Object);

            //Act
            processor.Process(file, "test");

            //Assert
            mockErrorsProcessingHandler.Verify(e => e.ProcessingErrors(file, "test", eventReaderResult), Times.Once);
        }
    }
}
