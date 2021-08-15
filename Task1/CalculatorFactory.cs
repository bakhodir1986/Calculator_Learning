namespace Calculator.Task1
{
    public class CalculatorFactory
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

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }
    }
}
