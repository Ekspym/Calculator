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
    public class CalculatorSwitcherTest
    {
        [Fact]
        public void CalculatorSwitch_DefaultCalculator_ReturnsDefaultCalculator()
        {
            // Arrange
            var loggerMock = new Mock<IErrorLogger>();
            var switcher = new CalculatorSwitcher(loggerMock.Object);

            // Act
            var calculator = switcher.CalculatorSwitch();

            // Assert
            Assert.IsType<DefaultCalculator>(calculator);
        }

        [Fact]
        public void CalculatorSwitch_WholeNumberCalculator_ReturnsWholeNumberCalculator()
        {
            // Arrange
            var loggerMock = new Mock<IErrorLogger>();
            var switcher = new CalculatorSwitcher(loggerMock.Object);
            switcher.SwitchCalcType(); // Switch to whole number calculator

            // Act
            var calculator = switcher.CalculatorSwitch();

            // Assert
            Assert.IsType<WholeNumberCalculator>(calculator);
        }

        [Fact]
        public void SwitchCalcType_SwitchesToWholeNumberCalculator()
        {
            // Arrange
            var loggerMock = new Mock<IErrorLogger>();
            var switcher = new CalculatorSwitcher(loggerMock.Object);

            // Act
            switcher.SwitchCalcType();

            // Assert
            Assert.Equal(1, switcher.getUsedCalculator());
        }

        [Fact]
        public void SwitchCalcType_SwitchesToDefaultCalculator()
        {
            // Arrange
            var loggerMock = new Mock<IErrorLogger>();
            var switcher = new CalculatorSwitcher(loggerMock.Object);

            // Act
            switcher.SwitchCalcType(); // Switch to whole number calculator
            switcher.SwitchCalcType(); // Switch back to default calculator

            // Assert
            Assert.Equal(0, switcher.getUsedCalculator());
        }

        [Fact]
        public void CalculatorSwitch_AfterSwitchingMultipleTimes_ReturnsCorrectCalculator()
        {
            // Arrange
            var loggerMock = new Mock<IErrorLogger>();
            var switcher = new CalculatorSwitcher(loggerMock.Object);

            // Act
            switcher.SwitchCalcType(); // Switch to whole number calculator
            switcher.SwitchCalcType(); // Switch back to default calculator
            switcher.SwitchCalcType(); // Switch to whole number calculator

            var calculator = switcher.CalculatorSwitch();

            // Assert
            Assert.IsType<WholeNumberCalculator>(calculator);
        }
    }
}
