namespace BettyGame.Abstractions
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public decimal? Balance { get; set; }

        public ServiceResult(bool isSuccess, string message, decimal? balance = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Balance = balance;
        }

        public static ServiceResult Success(string message) => new(true, message);
        public static ServiceResult Failure(string message) => new(false, message);
    }
}
