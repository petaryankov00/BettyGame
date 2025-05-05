namespace BettyGame.Models.Configurations
{
    public class BetOptions
    {
        public decimal SmallestBet { get; set; }

        public decimal BiggestBet { get; set; }

        public BetOutcome Loss { get; set; }

        public BetOutcome SmallWin { get; set; }

        public BetOutcome BigWin { get; set; }
    }
}
