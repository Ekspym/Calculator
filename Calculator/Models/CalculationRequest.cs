namespace Calculator.Models
{
    public class CalculationRequest
    {
        public decimal operand1 { get; set; }
        public decimal operand2 { get; set; }
        public string operation { get; set; }
    }
}
