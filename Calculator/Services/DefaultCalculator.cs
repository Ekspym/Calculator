using Calculator.Data;

namespace Calculator.Services
{
    /// <summary>
    /// Calculator class for handling basic calculator logic
    /// two operands only, with decimals
    /// using Logger to write erros into file
    /// </summary>
    public class DefaultCalculator : ICalculator
    {
        IErrorLogger errorLogger;
        public DefaultCalculator(IErrorLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }
        public decimal Add(decimal operand1, decimal operand2)
        {
            try
            {
                return operand1 + operand2;
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
                {
                    throw new ArgumentException("Cannot divide by zero.");
                }

                return operand1 / operand2;
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
                return operand1 *= operand2;
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 0;
            }
        }

        public decimal Subtract(decimal operand1, decimal operand2)
        {
            try
            {
                return operand1 - operand2;
            }
            catch (Exception ex)
            {
                errorLogger.SendError(ex);
                return 0;
            }
        }
    }
}
