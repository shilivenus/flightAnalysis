using System;
using System.Collections.Generic;
using System.Text;
using AirlineFlightDataService.Module;
using AirlineFlightDataService.Validator.Rules;
using AutoFixture;
using Xunit;

namespace AirlineFlightDataService.Tests.Validator
{
    public class FlightMatchingRuleTest
    {
        [Fact]
        public void ReturnTrueWhenFlightIsNotNull()
        {
            //Arrange
            var fixture = new Fixture();

            var flightEvent = fixture.Build<Event>()
                .With(x => x.Flight, "AAV732")
                .Create();

            var flightMatchingRule = new FlightMatchingRule();

            //Act
            var result = flightMatchingRule.IsMatched(flightEvent);

            //Assert
            Assert.True(result);
        }
    }
}
