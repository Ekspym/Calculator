namespace Calculator.Services
{
    public interface IErrorLogger
    {
        void SendError(Exception exception);

    }
}
