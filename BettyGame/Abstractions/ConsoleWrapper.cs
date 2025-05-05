using BettyGame.Abstractions.Interfaces;

namespace BettyGame.Abstractions
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public string? ReadLine() => Console.ReadLine();
    }
}
