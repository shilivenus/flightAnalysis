using System;
using System.Collections.Generic;
using AirlineFlightDataService.Logger;
using AirlineFlightDataService.LogWriter;
using AutoFixture;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventLogger
{
    public class EventLoggerTest
    {
        [Fact]
        public void LogWriteCalledOnceWhenNoEventInEventDetaisList()
        {
            //Arrange
            var eventDetailsList = new Dictionary<string, int>();
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);

            //Act
            logger.LogEventDetails(eventDetailsList);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole("There is no event been processed."), Times.Once);
        }

        [Fact]
        public void LogWriteCalledOnceWhenCallLogBatchProcessTime()
        {
            //Arrange
            var fixture = new Fixture();
            var ts = fixture.Build<TimeSpan>().Create();
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);

            //Act
            logger.LogBatchProcessTime(ts);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole($"This batch takes {ts} in total"), Times.Once);
        }

        [Fact]
        public void WriteErrorToConsoleWillBeCalledOnceWhenCallLogErrorToConsole()
        {
            //Arrange
            var fixture = new Fixture();
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);
            var e = fixture.Build<Exception>().Create();

            //Act
            logger.LogErrorToConsole(e, e.Message);

            //Assert
            mockLogWriter.Verify(l => l.WriteErrorToConsole(e, e.Message), Times.Once);
        }

        [Fact]
        public void WriteInfoToConsoleWithCertainStingWillBeCalledOnceWhenLogEventDetailsIsCalledWIthEmptyEventDetailsList()
        {
            //Arrange
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);
            var emptyEventDetailsList = new Dictionary<string, int>();

            //Act
            logger.LogEventDetails(emptyEventDetailsList);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole("There is no event been processed."), Times.Once);
        }

        [Fact]
        public void EventDetailsWillBeLogIntoConsoleWhenEventDetailsListIsNotEmpty()
        {
            //Arrange
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);
            var eventDetailsList = new Dictionary<string, int> {{"firstKey", 0}};

            //Act
            logger.LogEventDetails(eventDetailsList);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole("firstKey has 0"), Times.Once);
        }

        [Fact]
        public void NoFailedEventMessageWillBeLogIntoConsoleWhenCallLogFailedEventDetailsWithEmptyFailedEventListAndZeroTotalFailed()
        {
            //Arrange
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);
            var failedEventList = new List<string>();
            var totalFailed = 0;

            //Act
            logger.LogFailedEventDetails(failedEventList, totalFailed);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole("There is no failed event"), Times.Once);
        }

        [Fact]
        public void FailedEventDetailsWillBeLogIntoConsoleWhenCallLogFailedEventDetailsWithCorrectParamters()
        {
            //Arrange
            var failedEventList = new List<string>{"test", "failed"};
            var totalFailed = failedEventList.Count;
            var mockLogWriter = new Mock<ILogWriter>();
            var logger = new FlightEventLogger(mockLogWriter.Object);

            //Act
            logger.LogFailedEventDetails(failedEventList, totalFailed);

            //Assert
            mockLogWriter.Verify(l => l.WriteInfoToConsole($"Total Failed number is {totalFailed}"), Times.Once);
            mockLogWriter.Verify(l => l.WriteInfoToConsole("Failed events have been listed below"), Times.Once);
            mockLogWriter.Verify(l => l.WriteInfoToConsole(failedEventList[0]), Times.Once);
        }
    }
}