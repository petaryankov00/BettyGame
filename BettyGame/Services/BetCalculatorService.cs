using BettyGame.Abstractions;
using BettyGame.Abstractions.Interfaces;
using BettyGame.Common;
using BettyGame.Models.Configurations;
using Microsoft.Extensions.Options;

namespace BettyGame.Services
{
    public class BetCalculatorService : IBetCalculatorService
    {
        private readonly BetOptions _betOptions;
        private readonly IRandomGenerator _randomGenerator;

        public BetCalculatorService(IOptions<BetOptions> betOptions, IRandomGenerator randomGenerator)
        {
            _betOptions = betOptions.Value;
            _randomGenerator = randomGenerator;
        }

        public ServiceResult Calculate(decimal currentBalance, decimal betAmount)
        {
            double roll = _randomGenerator.NextDouble();

            if (roll < _betOptions.Loss.Chance)
            {
                currentBalance -= betAmount;

                return new ServiceResult(true, String.Format(Constants.BetLoseMessage, currentBalance), currentBalance);
            }
            else if (roll < _betOptions.Loss.Chance + _betOptions.SmallWin.Chance)
            {
                var win = Math.Round(betAmount * GetRandomMultiplier(_betOptions.SmallWin.WinRatioStart, _betOptions.SmallWin.WinRatioEnd), 2);
                currentBalance = currentBalance - betAmount + win;

                return new ServiceResult(true, String.Format(Constants.BetWinMessage, win, currentBalance), currentBalance);
            }
            else
            {
                var win = Math.Round(betAmount * GetRandomMultiplier(_betOptions.BigWin.WinRatioStart, _betOptions.BigWin.WinRatioEnd), 2);
                currentBalance = currentBalance - betAmount + win;

                return new ServiceResult(true, String.Format(Constants.BetWinMessage, win, currentBalance), currentBalance);
            }
        }

        private decimal GetRandomMultiplier(double min, double max)
        {
            var multiplier = min + _randomGenerator.NextDouble() * (max - min);
            return (decimal)multiplier;
        }
    }
}
