using Calculator.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorUnitTest
{
    public class WholeNumberCalculatorTest
    {
        [Fact]
        public void Add_ReturnsRoundedSum()
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new WholeNumberCalculator(errorLoggerMock.Object);

            // Act
            var result = calculator.Add(3.5m, 2.5m);

            // Assert
            Assert.Equal(6.0m, result);
            errorLoggerMock.VerifyNoOtherCalls(); // Ensure error logger was not called
        }

        [Fact]
        public void Divide_ReturnsRoundedQuotient()
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new WholeNumberCalculator(errorLoggerMock.Object);

            // Act
            var result = calculator.Divide(6.0m, 2.0m);

            // Assert
            Assert.Equal(3.0m, result);
            errorLoggerMock.VerifyNoOtherCalls();
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

        [Fact]
        public void Multiply_ReturnsRoundedProduct()
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new WholeNumberCalculator(errorLoggerMock.Object);

            // Act
            var result = calculator.Multiply(3.5m, 2.5m);

            // Assert
            Assert.Equal(9.0m, result);
            errorLoggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Subtract_ReturnsRoundedDifference()
        {
            // Arrange
            var errorLoggerMock = new Mock<IErrorLogger>();
            var calculator = new WholeNumberCalculator(errorLoggerMock.Object);

            // Act
            var result = calculator.Subtract(5.5m, 2.5m);

            // Assert
            Assert.Equal(3.0m, result);
            errorLoggerMock.VerifyNoOtherCalls();
        }
    }
}
