using Azure;
using Calculator.Data;
using Calculator.Services;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.Net.Http.Headers;

namespace Calculator.Controllers
{
    /// <summary>
    /// API for calculator logic
    /// </summary>
    [ApiController]
    [Route("api/calculator")]
    public class CalculatorController : ControllerBase
    {
        private int usedCalculator = 0;
        private readonly ILogger<CalculatorController> logger;
        private readonly ApplicationDbContext dbContext;
        private IEnumerable<ICalculator> calculator;
        private readonly ICalculatorSwitcher calculatorSwitcher;
        public CalculatorController(ICalculatorSwitcher calculatorSwitcher, IEnumerable<ICalculator> calculator, ApplicationDbContext dbContext, ILogger<CalculatorController> logger)
        {
            this.calculatorSwitcher = calculatorSwitcher;
            this.calculator = calculator;
            this.dbContext = dbContext;
            this.logger = logger;

            logger.LogInformation("Constructor init");

        }


        /// <summary>
        /// Endpoint for performing mathematical calculations.
        /// </summary>
        /// <param name="request">CalculationRequest object containing operands and operation.</param>
        /// <returns>ActionResult containing the result and calculation ID.</returns>
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                decimal result = 0;
                Calculation calculation = new Calculation();
                var service = calculator.FirstOrDefault(x => x.GetType() == calculatorSwitcher.CalculatorSwitch().GetType());
                logger.LogInformation(calculatorSwitcher.CalculatorSwitch().GetType().ToString());
                if (service != null)
                {

                    switch (request.operation)
                    {
                        case "+":
                            result = service.Add(request.operand1, request.operand2);
                            break;
                        case "-":
                            result = service.Subtract(request.operand1, request.operand2);
                            break;
                        case "*":
                            result = service.Multiply(request.operand1, request.operand2);
                            break;
                        case "/":
                            result = service.Divide(request.operand1, request.operand2);
                            break;
                        default: throw new ArgumentException("Invalid operation.");
                    }

                    calculation.Operand1 = request.operand1;
                    calculation.Operand2 = request.operand2;
                    calculation.Operator = request.operation;
                    calculation.Result = result;
                    calculation.Timestamp = DateTime.Now;


                    dbContext.Calculations.Add(calculation);
                    dbContext.SaveChanges();


                    logger.LogInformation("Request processed. Result: {Result}, CalculationId: {CalculationId}", result, calculation.Id);
                }

                return Ok(new { Result = result, CalculationId = calculation.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during calculation.");
                return StatusCode(500, new { Message = ex.Message });
            }
        }



        /// <summary>
        /// Retrieves the last 10 calculations from the database history.
        /// </summary>
        /// <returns>ActionResult containing the last 10 calculations in JSON format.</returns>
        [HttpGet("calculationHistory")]
        public IActionResult CalculationHistory()
        {
            try
            {
                List<Calculation> calculations = new List<Calculation>();

                calculations = dbContext.Calculations
                    .OrderByDescending(x => x.Id)
                    .Take(10)
                    .ToList();

                string lastTenCalcs = JsonConvert.SerializeObject(calculations, Newtonsoft.Json.Formatting.Indented);

                logger.LogInformation("Request processed. Last 10 calculations sent");
                return Ok(lastTenCalcs);



            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during calculation.");
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Switching which calculator is used
        /// </summary>
        /// <returns>ActionResult containing number to know which calculator is used</returns>
        [HttpGet("switchCalculator")]
        public IActionResult SwitchCalculator()
        {
            try
            {           
                calculatorSwitcher.SwitchCalcType();
                string result = JsonConvert.SerializeObject(calculatorSwitcher.getUsedCalculator(), Newtonsoft.Json.Formatting.Indented);

                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during calculation.");
                return StatusCode(500, new { Message = ex.Message });
            }
        }

    }
}
