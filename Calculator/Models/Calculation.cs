namespace Calculator.Models
{
    public class Calculation
    {
        public int Id { get; set; }
        public decimal Operand1 { get; set; }
        public decimal Operand2 { get; set; }
        public string Operator { get; set; }
        public decimal Result { get; set; }
        public int CalculatorType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
