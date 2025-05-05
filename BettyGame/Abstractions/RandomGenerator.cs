using BettyGame.Abstractions.Interfaces;

namespace BettyGame.Abstractions
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new();
        public double NextDouble() => _random.NextDouble();
    }
}
