namespace Calculator.Services
{
    public interface ICalculatorSwitcher
    {
        ICalculator CalculatorSwitch();

        int getUsedCalculator();

        void SwitchCalcType();
    }
}
