using BettyGame.Abstractions;
using BettyGame.Abstractions.Interfaces;
using BettyGame.Common;
using BettyGame.Services;
using BettyGame.Tests.Helpers;
using Moq;

namespace BettyGame.Tests
{
    public class PlayerWalletServiceTests
    {
        private readonly Mock<IBetCalculatorService> _betCalculatorMock = new();
        private readonly PlayerWalletService _playerWalletService;

        public PlayerWalletServiceTests()
        {
            _playerWalletService = new PlayerWalletService(ConfigHelper.GetBetOptions(), _betCalculatorMock.Object);
        }

        [Fact]
        public void Deposit_ValidAmount_ReturnsSuccess()
        {
            // Arrange
            decimal amount = 50;

            // Act
            var result = _playerWalletService.Deposit(amount);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(amount, _playerWalletService.Balance);
            Assert.Contains("Your deposit of $50 was successful", result.Message);
        }

        [Fact]
        public void Deposit_InvalidAmount_ReturnsFailure()
        {
            // Arrange
            decimal amount = -10;

            // Act
            var result = _playerWalletService.Deposit(amount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(Constants.AmountErrorMessage, result.Message);
        }

        [Fact]
        public void Withdrawal_ValidAmount_ReturnsSuccess()
        {
            // Arrange
            decimal depositAmount = 100;
            decimal withdrawalAmount = 50;
            _playerWalletService.Deposit(depositAmount);

            // Act
            var result = _playerWalletService.Withdrawal(withdrawalAmount);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(50, _playerWalletService.Balance);
            Assert.Contains("Your withdrawal of $50 was successful", result.Message);
        }

        [Fact]
        public void Withdrawal_AmountGreaterThanBalance_ReturnsFailure()
        {
            // Arrange
            decimal depositAmount = 50;
            decimal withdrawalAmount = 100;
            _playerWalletService.Deposit(depositAmount);

            // Act
            var result = _playerWalletService.Withdrawal(withdrawalAmount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Withdrawal failed: You attempted to withdrawal $100, but your current balance is only $50", result.Message);
        }

        [Fact]
        public void Bet_ValidBet_ReturnsSuccess()
        {
            // Arrange
            decimal depositAmount = 100;
            decimal betAmount = 10;

            _playerWalletService.Deposit(depositAmount);

            _betCalculatorMock.Setup(b => b.Calculate(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new ServiceResult(true, It.IsAny<string>(), depositAmount - betAmount + 40));

            // Act
            var result = _playerWalletService.Bet(betAmount);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Bet_AmountGreaterThanBalance_ReturnsFailure()
        {
            // Arrange
            decimal depositAmount = 50;
            decimal betAmount = 100;
            _playerWalletService.Deposit(depositAmount);

            // Act
            var result = _playerWalletService.Bet(betAmount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Bet failed: Either you do not have enough funds or your bet is out of range", result.Message);
        }

        [Fact]
        public void Bet_AmountNotInRange_ReturnsFailure()
        {
            // Arrange
            decimal depositAmount = 50;
            decimal betAmount = 12;
            _playerWalletService.Deposit(depositAmount);

            // Act
            var result = _playerWalletService.Bet(betAmount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Bet failed: Either you do not have enough funds or your bet is out of range", result.Message);
        }

        [Fact]
        public void Bet_InvalidAmount_ReturnsFailure()
        {
            // Arrange
            decimal betAmount = -5;

            // Act
            var result = _playerWalletService.Bet(betAmount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(Constants.AmountErrorMessage, result.Message);
        }
    }
}
