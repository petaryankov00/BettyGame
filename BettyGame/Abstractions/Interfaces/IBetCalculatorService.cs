namespace BettyGame.Abstractions.Interfaces
{
    public interface IBetCalculatorService
    {
        ServiceResult Calculate(decimal currentBalance, decimal betAmount);
    }
}
