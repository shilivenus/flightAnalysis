using System;
using System.Collections.Generic;
using System.Text;
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
            mockLogWriter.Verify(l => l.WriteToConsole("There is no event been processed."), Times.Once);
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
            mockLogWriter.Verify(l => l.WriteToConsole($"This batch takes {ts} in total"), Times.Once);
        }
    }
}
