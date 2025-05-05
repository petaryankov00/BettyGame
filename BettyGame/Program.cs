using BettyGame.Abstractions;
using BettyGame.Abstractions.Interfaces;
using BettyGame.Models.Configurations;
using BettyGame.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BettyGame
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Set up configuration
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // Set up DI
            var services = new ServiceCollection()
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IPlayerWalletService, PlayerWalletService>()
                .AddTransient<IBetCalculatorService, BetCalculatorService>()
                .AddTransient<IRandomGenerator, RandomGenerator>();

            services.Configure<BetOptions>(configuration.GetSection("BetOptions"));

            var provider = services.BuildServiceProvider();

            var game = provider.GetRequiredService<IGameService>();

            game.Play();
        }
    }
}
