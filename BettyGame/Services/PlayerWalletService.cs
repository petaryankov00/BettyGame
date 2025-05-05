using BettyGame.Abstractions;
using BettyGame.Abstractions.Interfaces;
using BettyGame.Common;
using BettyGame.Models.Configurations;
using Microsoft.Extensions.Options;

namespace BettyGame.Services
{
    public class PlayerWalletService : IPlayerWalletService
    {
        private decimal _balance = 0;
        private readonly BetOptions _betOptions;
        private readonly IBetCalculatorService _betCalculatorService;

        public PlayerWalletService(IOptions<BetOptions> betOptions, IBetCalculatorService betCalculatorService)
        {
            _betOptions = betOptions.Value;
            _betCalculatorService = betCalculatorService;
        }

        public decimal Balance { get => _balance; }

        public ServiceResult Deposit(decimal amount)
        {
            if (!IsValidAmount(amount))
            {
                return ServiceResult.Failure(Constants.AmountErrorMessage);
            }

            _balance += amount;

            return ServiceResult.Success(String.Format(Constants.DepositSuccessMessage, amount, _balance));
        }

        public ServiceResult Withdrawal(decimal amount)
        {
            if (!IsValidAmount(amount))
            {
                return ServiceResult.Failure(Constants.AmountErrorMessage);
            }

            if (amount > _balance)
            {
                return ServiceResult.Failure(String.Format(Constants.WithdrawalErrorMessage, amount, _balance));
            }

            _balance -= amount;

            return ServiceResult.Success(String.Format(Constants.WithdrawalSuccessMessage, amount, _balance));
        }

        public ServiceResult Bet(decimal amount)
        {
            if (!IsValidAmount(amount))
            {
                return ServiceResult.Failure(Constants.AmountErrorMessage);
            }

            if (amount > _balance || !IsValidBet(amount))
            {
                return ServiceResult.Failure(String.Format(Constants.BetErrorMessage, _betOptions.SmallestBet, _betOptions.BiggestBet));
            }

            var result = _betCalculatorService.Calculate(_balance, amount);

            _balance = result.Balance!.Value;

            return ServiceResult.Success(result.Message);
        }

        private static bool IsValidAmount(decimal amount)
            => amount > 0;

        private bool IsValidBet(decimal amount)
            => amount >= _betOptions.SmallestBet && amount <= _betOptions.BiggestBet;
    }
}
