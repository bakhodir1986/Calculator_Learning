namespace Calculator.Task3
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

        public ICalculator CreateCachedCalculator()
        {
            return new CachedPaymentDecorator();
        }

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }

        public ICalculator CreateLoggingCalculator()
        {
            return new LoggingCalculatorDecorator();
        }

        public ICalculator CreateRoundingCalculator()
        {
            return new RoundingCalculatorDecorator();
        }
    }
}
