using Calculator.Services;
using Moq;
using Xunit;

namespace CalculatorUnitTest
{
    public class DefaultCalculatorTest
    {
        private void TestOperation(Func<DefaultCalculator, decimal, decimal, decimal> operation, decimal operand1, decimal operand2, decimal expectedResult)
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new DefaultCalculator(errorLoggerMock.Object);

            // Act
            var result = operation(calculator, operand1, operand2);

            // Assert
            Assert.Equal(expectedResult, result);
            errorLoggerMock.VerifyNoOtherCalls(); // Ensure error logger was not called
        }

        [Fact]
        public void Add_ReturnsCorrectSum()
        {
            TestOperation((calculator, op1, op2) => calculator.Add(op1, op2), 3.5m, 2.5m, 6.0m);
        }

        [Fact]
        public void Subtract_ReturnsCorrectDifference()
        {
            TestOperation((calculator, op1, op2) => calculator.Subtract(op1, op2), 5.0m, 2.5m, 2.5m);
        }

        [Fact]
        public void Multiply_ReturnsCorrectProduct()
        {
            TestOperation((calculator, op1, op2) => calculator.Multiply(op1, op2), 3.0m, 2.0m, 6.0m);
        }

        [Fact]
        public void Divide_ReturnsCorrectQuotient()
        {
            TestOperation((calculator, op1, op2) => calculator.Divide(op1, op2), 6.0m, 2.0m, 3.0m);
        }

 
        [Fact]
        public void Divide_ByZeroThrowsException()
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new DefaultCalculator(errorLoggerMock.Object);

            // Act
            var result = calculator.Divide(6.0m, 0.0m);

            // Assert
            Assert.Equal(999999999999999, result);

            // Verify that the exception is logged
            errorLoggerMock.Verify(logger => logger.SendError(It.IsAny<ArgumentException>()), Times.Once);

        }
    }
}