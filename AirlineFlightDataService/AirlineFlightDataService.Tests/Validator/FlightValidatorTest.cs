using System.Collections.Generic;
using AirlineFlightDataService.Business.Module;
using AirlineFlightDataService.Business.Validator;
using AirlineFlightDataService.Business.Validator.Rules;
using AutoFixture;
using Moq;
using Xunit;

namespace AirlineFlightDataService.Tests.Validator
{
    public class FlightValidatorTest
    {
        [Fact]
        public void ReturnFalseWhenOneRuleFail()
        {
            //Arrange
            var mockRule1 = new Mock<IRule>();
            mockRule1.Setup(r => r.IsMatched(It.IsAny<Event>())).Returns(false);

            var mockRule2 = new Mock<IRule>();
            mockRule2.Setup(r => r.IsMatched(It.IsAny<Event>())).Returns(true);

            List<IRule> rules = new List<IRule> {mockRule1.Object, mockRule2.Object};

            FlightValidator validator = new FlightValidator(rules);

            var fixture = new Fixture();

            var flightEvent = fixture.Build<Event>().Create();

            //Act
            var result = validator.IsValidate(flightEvent);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ReturnTrueWhenAllRulePass()
        {
            //Arrange
            var mockRule1 = new Mock<IRule>();
            mockRule1.Setup(r => r.IsMatched(It.IsAny<Event>())).Returns(true);

            var mockRule2 = new Mock<IRule>();
            mockRule2.Setup(r => r.IsMatched(It.IsAny<Event>())).Returns(true);

            List<IRule> rules = new List<IRule> { mockRule1.Object, mockRule2.Object };

            FlightValidator validator = new FlightValidator(rules);

            var fixture = new Fixture();

            var flightEvent = fixture.Build<Event>().Create();

            //Act
            var result = validator.IsValidate(flightEvent);

            //Assert
            Assert.True(result);
        }
    }
}
