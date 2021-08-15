using System;
using System.Collections.Generic;

namespace Calculator.Task4
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

    public class RoundingCalculatorDecorator : ICalculator
    {
        private readonly ICalculator insurancePaymentCalculator;

        public RoundingCalculatorDecorator(ICalculator calculator)
        {
            insurancePaymentCalculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            var payment = Math.Round(insurancePaymentCalculator.CalculatePayment(touristName));

            return payment;
        }
    }

    public class LoggingCalculatorDecorator : ICalculator
    {
        private readonly ICalculator insurancePaymentCalculator;
        private ILogger _logger { get; set; }

        public LoggingCalculatorDecorator(ICalculator calculator, ILogger logger)
        {
            insurancePaymentCalculator = calculator;
            _logger = logger;
        }

        public decimal CalculatePayment(string touristName)
        {
            _logger.Log("Start");

            var payment = insurancePaymentCalculator.CalculatePayment(touristName);

            _logger.Log("End");

            return payment;
        }
    }

    public class CachedPaymentDecorator : ICalculator
    {
        private readonly ICalculator insurancePaymentCalculator;
        private readonly Dictionary<string, decimal> cacheOfPaymentByTourist;

        public CachedPaymentDecorator(ICalculator calculator)
        {
            insurancePaymentCalculator = calculator;
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
