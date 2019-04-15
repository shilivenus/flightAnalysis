using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using Xunit;

namespace AirlineFlightDataService.Tests.Logger
{
    public class LoggerTest
    {
        [Fact]
        public void CanLogEventDetailsWhenEventListIsNotEmpty()
        {
            //Arrange
            var fixture = new Fixture();

            var eventDetailsList = fixture.CreateMany<KeyValuePair<string, int>>(20)
                .ToDictionary(x => x.Key, x => x.Value);

            fixture.Register(() => eventDetailsList);
        }
    }
}
