using BettyGame.Abstractions.Interfaces;
using BettyGame.Models.Configurations;
using BettyGame.Services;
using BettyGame.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace BettyGame.Tests
{
    public class BetCalculatorServiceTests
    {
        private readonly Mock<IRandomGenerator> _randomGeneratorMock = new();
        private readonly IOptions<BetOptions> _betOptions = ConfigHelper.GetBetOptions();
        private readonly BetCalculatorService _betCalculator;

        public BetCalculatorServiceTests()
        {
            _betCalculator = new BetCalculatorService(_betOptions, _randomGeneratorMock.Object);
        }

        [Fact]
        public void Calculate_LossCase_DecreasesBalance()
        {
            //Arrange
            _randomGeneratorMock.Setup(r => r.NextDouble()).Returns(0.4);

            //Act
            var result = _betCalculator.Calculate(100, 10);

            //Assert
            Assert.Equal(90, result.Balance);
        }

        [Fact]
        public void Calculate_SmallWinCase_IncreasesBalanceWithMultiplier()
        {
            //Arrange
            _randomGeneratorMock.SetupSequence(r => r.NextDouble())
                .Returns(0.6) //Small win case
                .Returns(0.5); //Used to calculate multiplier

            var multiplier = (decimal)(_betOptions.Value.SmallWin.WinRatioStart + 0.5 * (_betOptions.Value.SmallWin.WinRatioEnd - _betOptions.Value.SmallWin.WinRatioStart));
            decimal win = Math.Round(10 * multiplier, 2);
            decimal expected = 100 - 10 + win;

            //Act
            var result = _betCalculator.Calculate(100, 10);

            //Assert
            Assert.Equal(expected, result.Balance);
        }

        [Fact]
        public void Calculate_BigWinCase_IncreasesBalanceWithBigMultiplier()
        {
            //Arrange
            _randomGeneratorMock.SetupSequence(r => r.NextDouble())
                .Returns(0.95)  // Big win case
                .Returns(0.25); // Used to calculate multiplier

            var multiplier = (decimal)(_betOptions.Value.BigWin.WinRatioStart + 0.25 * (_betOptions.Value.BigWin.WinRatioEnd - _betOptions.Value.BigWin.WinRatioStart));
            var win = Math.Round(10 * multiplier, 2);
            var expected = 100 - 10 + win;

            //Act
            var result = _betCalculator.Calculate(100, 10);

            //Assert
            Assert.Equal(expected, result.Balance);
        }

    }
}
