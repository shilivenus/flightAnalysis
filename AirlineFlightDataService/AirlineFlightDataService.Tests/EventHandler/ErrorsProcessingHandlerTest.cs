using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.EventHandler;
using AirlineFlightDataService.Module;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.EventHandler
{
    public class ErrorsProcessingHandlerTest
    {
        [Fact]
        public void ThrowExceptionWhenDirectoryDoesNotExist()
        {
            //Arrange
            var fixture = new Fixture();

            var file = fixture.Build<string>().Create();
            var result = fixture.Build<EventReaderResult>().Create();

            var mockConfig = new Mock<IConfiguration>();

            mockConfig.Setup(c => c["exceptionFolder"]).Returns("Test");

            var errorProcessingHandler = new ErrorsProcessingHandler(mockConfig.Object);

            //Assert
            Assert.Throws<Exception>(() => errorProcessingHandler.ProcessingErrors(file, "test", result));
        }
    }
}
