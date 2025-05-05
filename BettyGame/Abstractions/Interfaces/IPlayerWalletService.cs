namespace BettyGame.Abstractions.Interfaces
{
    public interface IPlayerWalletService
    {
        decimal Balance { get; }

        ServiceResult Deposit(decimal amount);

        ServiceResult Withdrawal(decimal amount);

        ServiceResult Bet(decimal amount);
    }
}
