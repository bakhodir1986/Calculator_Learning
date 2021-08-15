using System;

namespace Calculator.Task1
{
    public class InsurancePaymentCalculator : ICalculator
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public InsurancePaymentCalculator(
            ICurrencyService currencyService,
            ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public decimal CalculatePayment(string touristName)
        {
            var tripDetails = tripRepository.LoadTrip(touristName);

            var rate = currencyService.LoadCurrencyRate();

            return Constants.A * rate * tripDetails.FlyCost
                 + Constants.B * rate * tripDetails.AccomodationCost
                 + Constants.C * rate * tripDetails.ExcursionCost;

        }
    }
}
