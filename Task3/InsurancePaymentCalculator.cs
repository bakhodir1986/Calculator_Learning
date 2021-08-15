using System;

namespace Calculator.Task3
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

    public class RoundingCalculatorDecorator : ICalculator
    {
        public RoundingCalculatorDecorator()
        {
        }

        public decimal CalculatePayment(string touristName)
        {
            throw new NotImplementedException();
        }
    }

    public class LoggingCalculatorDecorator : ICalculator
    {
        public LoggingCalculatorDecorator()
        {
        }

        public decimal CalculatePayment(string touristName)
        {
            throw new NotImplementedException();
        }
    }

    public class CachedPaymentDecorator : ICalculator
    {
        public CachedPaymentDecorator()
        {
        }

        public decimal CalculatePayment(string touristName)
        {
            throw new NotImplementedException();
        }
    }
}
