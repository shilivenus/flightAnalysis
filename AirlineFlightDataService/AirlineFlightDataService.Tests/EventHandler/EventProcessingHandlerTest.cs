using System;
using System.Collections.Generic;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Validator;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventHandler
{
    public class EventProcessingHandlerTest
    {
        [Fact]
        public void LoggerWillBeCalledOnceWhenConfigIsEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetails = fixture.Build<EventDetails>().Create();
            var result = fixture.Build<EventReaderResult>().Create();
            var flightEventTypeName = result.Events[0].EventType.ToLower();

            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockValidator = new Mock<IValidator>();

            mockConfig.Setup(c => c.GetSection("test").GetChildren()).Returns(new List<IConfigurationSection>());

            var eventProcessingHandler =
                new EventProcessingHandler(mockConfig.Object, mockValidator.Object, mockLogger.Object);

            //Act
            eventProcessingHandler.ProcessingEvent(eventDetails, result);

            //Assert
            mockLogger.Verify(
                l => l.LogInfoToConsole($"{flightEventTypeName} has no folder path setup in appsettings.json"),
                Times.Once);
        }

        [Fact]
        public void ValidatorWillBeCalledOnceWhenConfigIsNotEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetails = fixture.Build<EventDetails>().Create();
            var result = fixture.Build<EventReaderResult>().Create();

            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockValidator = new Mock<IValidator>();
            var fakeData = new Dictionary<string, string>();
            fakeData.Add("test", "test value");

            var fakeConfig = new List<IConfigurationSection> {new FakeConfigSection(fakeData)};

            mockConfig.Setup(c => c.GetSection("test").GetChildren()).Returns(fakeConfig);

            var eventProcessingHandler =
                new EventProcessingHandler(mockConfig.Object, mockValidator.Object, mockLogger.Object);

            //Act
            eventProcessingHandler.ProcessingEvent(eventDetails, result);

            //Assert
            mockValidator.Verify(v => v.IsValidate(result.Events[0]), Times.Once);
        }

        [Fact]
        public void LoggerWillBeCalledOnceWhenConfigIsNotEmptyAndEventIsValidAndCuratedFolderIsEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetails = fixture.Build<EventDetails>().Create();
            var result = fixture.Build<EventReaderResult>().Create();

            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockValidator = new Mock<IValidator>();
            var fakeData = new Dictionary<string, string>();
            fakeData.Add("curated", "");

            var fakeConfig = new List<IConfigurationSection> { new FakeConfigSection(fakeData) };

            mockConfig.Setup(c => c.GetSection(result.Events[0].EventType.ToLower()).GetChildren()).Returns(fakeConfig);
            mockValidator.Setup(v => v.IsValidate(result.Events[0])).Returns(true);

            var eventProcessingHandler =
                new EventProcessingHandler(mockConfig.Object, mockValidator.Object, mockLogger.Object);

            //Act
            eventProcessingHandler.ProcessingEvent(eventDetails, result);

            //Assert
            mockLogger.Verify(l => l.LogInfoToConsole($"{result.Events[0].EventType.ToLower()} has no curated folder setup in appsettings.json."), Times.Once);
        }

        [Fact]
        public void LoggerWillBeCalledOnceWhenConfigIsNotEmptyAndEventIsValidAndExceptionFolderIsEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetails = fixture.Build<EventDetails>().Create();
            var result = fixture.Build<EventReaderResult>().Create();

            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockValidator = new Mock<IValidator>();
            var fakeData = new Dictionary<string, string>();
            fakeData.Add("exception", "");

            var fakeConfig = new List<IConfigurationSection> { new FakeConfigSection(fakeData) };

            mockConfig.Setup(c => c.GetSection(result.Events[0].EventType.ToLower()).GetChildren()).Returns(fakeConfig);
            mockValidator.Setup(v => v.IsValidate(result.Events[0])).Returns(false);

            var eventProcessingHandler =
                new EventProcessingHandler(mockConfig.Object, mockValidator.Object, mockLogger.Object);

            //Act
            eventProcessingHandler.ProcessingEvent(eventDetails, result);

            //Assert
            mockLogger.Verify(l => l.LogInfoToConsole($"{result.Events[0].EventType.ToLower()} has no exception folder setup in appsettings.json."), Times.Once);
        }

        [Fact]
        public void ExceptionWillBeThrowWhenConfigIsNotEmptyAndEventIsValidAndExceptionFolderIsNotEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetails = fixture.Build<EventDetails>().Create();
            var result = fixture.Build<EventReaderResult>().Create();

            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockValidator = new Mock<IValidator>();
            var fakeData = new Dictionary<string, string>();
            fakeData.Add("exception", "test");

            var fakeConfig = new List<IConfigurationSection> { new FakeConfigSection(fakeData, "exception", "test") };

            mockConfig.Setup(c => c.GetSection(result.Events[0].EventType.ToLower()).GetChildren()).Returns(fakeConfig);
            mockValidator.Setup(v => v.IsValidate(result.Events[0])).Returns(false);

            var eventProcessingHandler =
                new EventProcessingHandler(mockConfig.Object, mockValidator.Object, mockLogger.Object);

            //Assert
            Assert.Throws<Exception>(() => eventProcessingHandler.ProcessingEvent(eventDetails, result));
        }
    }
}
