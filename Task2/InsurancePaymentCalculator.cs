using System;
using System.Collections.Generic;

namespace Calculator.Task2
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

    public class CachedInsurancePaymentCalculator : ICalculator
    {
        private readonly InsurancePaymentCalculator insurancePaymentCalculator;
        private readonly Dictionary<string, decimal> cacheOfPaymentByTourist;

        public CachedInsurancePaymentCalculator(ICurrencyService currencyService,
            ITripRepository tripRepository) 
        {
            insurancePaymentCalculator = new InsurancePaymentCalculator(currencyService,
                tripRepository);
            cacheOfPaymentByTourist = new Dictionary<string, decimal>();
        }

        public decimal CalculatePayment(string touristName)
        {
            if (cacheOfPaymentByTourist.ContainsKey(touristName))
            {
                return cacheOfPaymentByTourist[touristName];
            }

            var payment = insurancePaymentCalculator.CalculatePayment(touristName);

            cacheOfPaymentByTourist.Add(touristName, payment);

            return payment;
        }
    }
}
