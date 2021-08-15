namespace Calculator.Task4
{
    public interface ICalculatorFactory
    {
        ICalculator CreateCalculator();

        ICalculator CreateCalculator(bool withLogging, bool withCaching, bool withRounding);
    }
}
