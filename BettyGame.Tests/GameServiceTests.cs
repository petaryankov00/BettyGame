using BettyGame.Abstractions;
using BettyGame.Abstractions.Interfaces;
using BettyGame.Services;
using Moq;

namespace BettyGame.Tests
{
    public class GameServiceTests
    {
        private readonly Mock<IPlayerWalletService> _walletServiceMock = new();
        private readonly Mock<IConsoleWrapper> _consoleMock = new();
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _gameService = new GameService(_walletServiceMock.Object, _consoleMock.Object);
        }

        [Theory]
        [InlineData("bet 50", nameof(IPlayerWalletService.Bet))]
        [InlineData("deposit 75", nameof(IPlayerWalletService.Deposit))]
        [InlineData("withdrawal 20", nameof(IPlayerWalletService.Withdrawal))]
        public void Play_ValidInput_CallsExpectedWalletMethod_AndWritesMessage(string input, string expectedMethod)
        {
            // Arrange
            var inputs = new Queue<string>([input, "exit"]);
            _consoleMock.Setup(c => c.ReadLine()).Returns(() => inputs.Dequeue());
            _consoleMock.Setup(c => c.WriteLine(It.IsAny<string>()));

            var amount = decimal.Parse(input.Split(' ')[1]);
            var serviceResult = new ServiceResult(true, $"{expectedMethod} called successfully.");

            switch (expectedMethod)
            {
                case nameof(IPlayerWalletService.Bet):
                    _walletServiceMock.Setup(s => s.Bet(amount)).Returns(serviceResult);
                    break;
                case nameof(IPlayerWalletService.Deposit):
                    _walletServiceMock.Setup(s => s.Deposit(amount)).Returns(serviceResult);
                    break;
                case nameof(IPlayerWalletService.Withdrawal):
                    _walletServiceMock.Setup(s => s.Withdrawal(amount)).Returns(serviceResult);
                    break;
            }

            // Act
            _gameService.Play();

            // Assert
            switch (expectedMethod)
            {
                case nameof(IPlayerWalletService.Bet):
                    _walletServiceMock.Verify(s => s.Bet(amount), Times.Once);
                    break;
                case nameof(IPlayerWalletService.Deposit):
                    _walletServiceMock.Verify(s => s.Deposit(amount), Times.Once);
                    break;
                case nameof(IPlayerWalletService.Withdrawal):
                    _walletServiceMock.Verify(s => s.Withdrawal(amount), Times.Once);
                    break;
            }

            _consoleMock.Verify(c => c.WriteLine(serviceResult.Message), Times.Once);
        }
    }
}
