using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public CalculatorSwitcher(IErrorLogger logger, IConfiguration configuration) 
        {
            this.logger = logger;
            this.configuration = configuration;
            initCalculators();
        }

        private void initCalculators()
        {
            this.calculator = new DefaultCalculator(logger);
            this.wholeNumberCalculator = new WholeNumberCalculator(logger);
            this.usedCalculator = configuration.GetValue<int>("ControllerSettings:UsedCalculator");
        }

        public ICalculator CalculatorSwitch()
        {
            switch (usedCalculator)
            {
                case 0:
                    return this.calculator;
                    break;
                case 1:
                    return this.wholeNumberCalculator;
                    break;
                default:
                    return this.calculator;
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
            this.usedCalculator = 0;
        }

        private void SwitchToWholeNumberCalculator()
        {
            this.usedCalculator = 1;
        }

        public int getUsedCalculator()
        {
            return usedCalculator;
        }


    }
}
