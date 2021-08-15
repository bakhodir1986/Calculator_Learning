using System;
using System.Collections.Generic;

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
            var tripDetails = tripRepository.LoadTrip(touristName);

            var rate = currencyService.LoadCurrencyRate();

            return Constants.A * rate * tripDetails.FlyCost
                 + Constants.B * rate * tripDetails.AccomodationCost
                 + Constants.C * rate * tripDetails.ExcursionCost;
        }
    }

    public class RoundingCalculatorDecorator : ICalculator
    {
        private readonly LoggingCalculatorDecorator loggingCalculatorDecorator;

        public RoundingCalculatorDecorator(ICurrencyService currencyService,
            ITripRepository tripRepository, ILogger logger)
        {
            loggingCalculatorDecorator = new LoggingCalculatorDecorator(currencyService,
                tripRepository, logger);
        }

        public decimal CalculatePayment(string touristName)
        {
            var payment = Math.Round(loggingCalculatorDecorator.CalculatePayment(touristName));

            return payment;
        }
    }

    public class LoggingCalculatorDecorator : ICalculator
    {
        private readonly CachedPaymentDecorator cachedPaymentDecorator;
        private readonly ILogger _logger;

        public LoggingCalculatorDecorator(ICurrencyService currencyService,
            ITripRepository tripRepository, ILogger logger)
        {
            cachedPaymentDecorator = new CachedPaymentDecorator(currencyService,
                tripRepository);
            _logger = logger;
        }

        public decimal CalculatePayment(string touristName)
        {
            _logger.Log("Start");

            var payment = cachedPaymentDecorator.CalculatePayment(touristName);

            _logger.Log("End");

            return payment;
        }
    }

    public class CachedPaymentDecorator : ICalculator
    {
        private readonly InsurancePaymentCalculator insurancePaymentCalculator;
        private readonly Dictionary<string, decimal> cacheOfPaymentByTourist;

        public CachedPaymentDecorator(ICurrencyService currencyService,
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
