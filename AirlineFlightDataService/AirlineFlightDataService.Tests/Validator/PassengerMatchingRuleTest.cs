using AirlineFlightDataService.Module;
using AirlineFlightDataService.Validator.Rules;
using AutoFixture;
using Xunit;

namespace AirlineFlightDataService.Tests.Validator
{
    public class PassengerMatchingRuleTest
    {
        [Fact]
        public void ReturnTrueWhenPassengerNumberIsLargerThanZero()
        {
            //Arrange
            var fixture = new Fixture();

            var flightEvent = fixture.Build<Event>()
                .With(x => x.Passengers, 1)
                .Create();

            var passengerMatchingRule = new PassengerMatchingRule();

            //Act
            var result = passengerMatchingRule.IsMatched(flightEvent);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ReturnFalseWhenPassengerNumberIsLessThanZero()
        {
            //Arrange
            var fixture = new Fixture();

            var flightEvent = fixture.Build<Event>()
                .With(x => x.Passengers, -1)
                .Create();

            var passengerMatchingRule = new PassengerMatchingRule();

            //Act
            var result = passengerMatchingRule.IsMatched(flightEvent);

            //Assert
            Assert.False(result);
        }
    }
}
