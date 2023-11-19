using Serilog;
namespace Calculator.Services
{
    /// <summary>
    /// basic logger using Serilog to log into file
    /// </summary>
    public class ErrorLogger : IErrorLogger
    {
       
        public void SendError(Exception exception)
        {
            Log.Error(exception.Message);
        }
    }
}
