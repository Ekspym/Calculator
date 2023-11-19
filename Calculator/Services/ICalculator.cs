namespace Calculator.Services
{
    public interface ICalculator
    {
        decimal Add(decimal operand1, decimal operand2);
        decimal Subtract(decimal operand1, decimal operand2);
        decimal Multiply(decimal operand1, decimal operand2);
        decimal Divide(decimal operand1, decimal operand2);
    }
}
