using BettyGame.Abstractions.Interfaces;

namespace BettyGame.Services
{
    public class GameService : IGameService
    {
        private readonly List<string> _validCommands = ["bet", "withdrawal", "deposit"];
        private readonly IPlayerWalletService _playerWalletService;
        private readonly IConsoleWrapper _consoleWrapper;

        public GameService(IPlayerWalletService playerWalletService, IConsoleWrapper consoleWrapper)
        {
            _playerWalletService = playerWalletService;
            _consoleWrapper = consoleWrapper;
        }

        public void Play()
        {
            try
            {
                while (true)
                {
                    _consoleWrapper.WriteLine("Please, submit action:");
                    var input = _consoleWrapper.ReadLine();

                    if (!ValidateUserInput(input, out var command, out var amount))
                    {
                        _consoleWrapper.WriteLine("Invalid input. Format should be: <command> <amount>. Example: bet 5");
                        continue;
                    }

                    switch (command)
                    {
                        case "bet":
                            _consoleWrapper.WriteLine(_playerWalletService.Bet(amount).Message);
                            break;

                        case "deposit":
                            _consoleWrapper.WriteLine(_playerWalletService.Deposit(amount).Message);
                            break;

                        case "withdrawal":
                            _consoleWrapper.WriteLine(_playerWalletService.Withdrawal(amount).Message);
                            break;

                        case "exit":
                            Console.WriteLine("Thank you for playing. Hope to see you again soon.");
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private bool ValidateUserInput(string? input, out string command, out decimal amount)
        {
            command = string.Empty;
            amount = 0;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts[0] == "exit")
            {
                command = parts[0];
                return true;
            }

            if (parts.Length != 2)
                return false;

            command = parts[0].ToLower();

            if (!_validCommands.Contains(command))
                return false;

            return Decimal.TryParse(parts[1], out amount);
        }
    }
}