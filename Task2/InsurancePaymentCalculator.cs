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
            throw new NotImplementedException();
        }
    }

    public class CachedInsurancePaymentCalculator : ICalculator
    {
        public CachedInsurancePaymentCalculator()
        {
        }

        public decimal CalculatePayment(string touristName)
        {
            throw new NotImplementedException();
        }
    }
}
