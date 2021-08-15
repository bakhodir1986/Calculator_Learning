using System;

namespace Calculator.Task4
{
    public class CalculatorFactory : ICalculatorFactory
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;
        private ILogger logger;

        public CalculatorFactory(
            ICurrencyService currencyService,
            ITripRepository tripRepository,
            ILogger logger)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
            this.logger = logger;
        }

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }

        public ICalculator CreateCalculator(bool withLogging, bool withCaching, bool withRounding)
        {
            ICalculator source = new InsurancePaymentCalculator(currencyService, tripRepository);

            if (withCaching && withRounding && withLogging)
            {
                source = new RoundingCalculatorDecorator(new CachedPaymentDecorator(new LoggingCalculatorDecorator(source, logger)));
            }
            else if (withLogging && withCaching)
            {
                source = new CachedPaymentDecorator(new LoggingCalculatorDecorator(source, logger));
            }
            else if (withLogging && withRounding)
            {
                source = new RoundingCalculatorDecorator(new LoggingCalculatorDecorator(source, logger));
            }
            else if (withCaching && withRounding)
            {
                source = new RoundingCalculatorDecorator(new CachedPaymentDecorator(source));
            }
            else if(withLogging)
            {
                source = new LoggingCalculatorDecorator(source, logger);
            }
            else if (withCaching)
            {
                source = new CachedPaymentDecorator(source);
            }
            else if (withRounding)
            {
                source = new RoundingCalculatorDecorator(source);
            }

            return source;
        }
    }
}
