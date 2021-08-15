namespace Calculator.Task2
{
    public class CalculatorFactory : ICalculatorFactory
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public CalculatorFactory(
            ICurrencyService currencyService,
            ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public ICalculator CreateCachedCalculator()
        {
            return new CachedInsurancePaymentCalculator();
        }

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }
    }
}
