namespace Calculator.Services
{
    /// <summary>
    /// Calculator class for handling basic calculator logic
    /// two operands only with decimals
    /// using Logger to write erros into file
    /// </summary>
    public class WholeNumberCalculator : ICalculator
    {
        IErrorLogger errorLogger;
        public WholeNumberCalculator(IErrorLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }
        public decimal Add(decimal operand1, decimal operand2)
        {
            try
            {
                return Math.Round(operand1 + operand2);
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 0;
            }
        }

        public decimal Divide(decimal operand1, decimal operand2)
        {
            try
            {
                if (operand2 == 0)
                    throw new ArgumentException("Cannot divide by zero.");

                decimal result = operand1 / operand2;
                return Math.Round(result);
            }
            catch (ArgumentException e)
            {
                errorLogger.SendError(e);
                return 999999999999999;
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 0;
            }
        }

        public decimal Multiply(decimal operand1, decimal operand2)
        {
            try
            {
                return Math.Round(operand1 * operand2);
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 999999999999999;
            }
        }

        public decimal Subtract(decimal operand1, decimal operand2)
        {
            try
            {
                return Math.Round(operand1 - operand2);
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 0;
            }
        }
    }
}
