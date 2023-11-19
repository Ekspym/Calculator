using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Calculator.Services
{
    /// <summary>
    /// CalculatorSwitcher for switching calculator logic during runtime
    /// int usedCalculator for keeping track which calculator is used
    /// 0 = default decimal calculator
    /// 1 = calculator that return only whole numbers
    /// </summary>
    public class CalculatorSwitcher : ICalculatorSwitcher
    {
        private int usedCalculator = 0;
        private IErrorLogger logger;
        private DefaultCalculator calculator;
        private WholeNumberCalculator wholeNumberCalculator;

        public CalculatorSwitcher(IErrorLogger logger) 
        {
            this.logger = logger;
            initCalculators();
        }

        private void initCalculators()
        {
            calculator = new DefaultCalculator(logger);
            wholeNumberCalculator = new WholeNumberCalculator(logger);
        }

        public ICalculator CalculatorSwitch()
        {
            switch (usedCalculator)
            {
                case 0:
                    return calculator;
                    break;
                case 1:
                    return wholeNumberCalculator;
                    break;
                default:
                    return calculator;
                    break;
            }
        }


        public void SwitchCalcType()
        {
            switch (usedCalculator)
            {
                case 0:
                    SwitchToWholeNumberCalculator();
                    break;
                case 1:
                    SwitchToDefaultCalculator();
                    break;
                default:
                    SwitchToDefaultCalculator();
                    break;
         
            }
        }

        private void SwitchToDefaultCalculator()
        {
            usedCalculator = 0;
        }

        private void SwitchToWholeNumberCalculator()
        {
            usedCalculator = 1;
        }

        public int getUsedCalculator()
        {
            return usedCalculator;
        }


    }
}
