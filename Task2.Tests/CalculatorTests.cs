using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Task2.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private decimal rate = 28.56M;
        private string touristName = "Vasia";
        private decimal flyCost = 1000M;
        private decimal accomodationCost = 550M;
        private decimal excursionCost = 720M;

        private Mock<ICurrencyService> currencyServiceMock;
        private Mock<ITripRepository> tripRepositoryMock;

        [TestInitialize]
        public void Set_up()
        {
            currencyServiceMock = new Mock<ICurrencyService>();
            tripRepositoryMock = new Mock<ITripRepository>();     
        }

        [TestMethod]
        public void Should_calculate_insurance_payment()
        {
            Given_currency_rate(rate);

            Given_tourist_trip(touristName, flyCost, accomodationCost, excursionCost);

            var insurancePayment = CreateCalculator().CalculatePayment(touristName);

            insurancePayment.Should().Be(59853.1920M);
        }

        [TestMethod]
        public void Should_cache_insurance_payment()
        {
            Given_currency_rate(rate);

            Given_tourist_trip(touristName, flyCost, accomodationCost, excursionCost);

            var calculator = CreateCachedCalculator();

            calculator.CalculatePayment(touristName).Should().Be(59853.1920M);

            currencyServiceMock.Verify(c => c.LoadCurrencyRate(), Times.Once);

            tripRepositoryMock.Verify(c => c.LoadTrip(touristName), Times.Once);

            calculator.CalculatePayment(touristName).Should().Be(59853.1920M);

            currencyServiceMock.Verify(c => c.LoadCurrencyRate(), Times.Once);

            tripRepositoryMock.Verify(c => c.LoadTrip(touristName), Times.Once);
        }

        private void Given_currency_rate(decimal rate)
        {
            currencyServiceMock.Setup(c => c.LoadCurrencyRate()).Returns(rate);
        }

        private void Given_tourist_trip(string touristName, decimal flyCost, decimal accomodationCost, decimal excursionCost)
        {
            tripRepositoryMock
               .Setup(c => c.LoadTrip(touristName))
               .Returns(new TripDetails
               {
                   TouristName = touristName,
                   FlyCost = flyCost,
                   AccomodationCost = accomodationCost,
                   ExcursionCost = excursionCost
               });
        }

        private ICalculator CreateCalculator()
        {
            return new CalculatorFactory(currencyServiceMock.Object, tripRepositoryMock.Object).CreateCalculator();
        }

        private ICalculator CreateCachedCalculator()
        {
            return new CalculatorFactory(currencyServiceMock.Object, tripRepositoryMock.Object).CreateCachedCalculator();
        }
    }
}
