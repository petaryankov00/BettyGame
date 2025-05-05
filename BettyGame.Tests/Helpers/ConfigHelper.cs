using BettyGame.Models.Configurations;
using Microsoft.Extensions.Options;

namespace BettyGame.Tests.Helpers
{
    public static class ConfigHelper
    {
        public static IOptions<BetOptions> GetBetOptions()
        {
            return Options.Create(new BetOptions
            {
                SmallestBet = 1.00m,
                BiggestBet = 10.00m,
                Loss = new()
                {
                    Chance = 0.5
                },
                SmallWin = new()
                {
                    Chance = 0.4,
                    WinRatioStart = 1.01,
                    WinRatioEnd = 2.00
                },
                BigWin = new()
                {
                    Chance = 0.1,
                    WinRatioStart = 2.01,
                    WinRatioEnd = 10.00
                }
            });
        }
    }
}
