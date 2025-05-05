namespace BettyGame.Common
{
    public static class Constants
    {
        public const string AmountErrorMessage = "Amount should be a positive number";
        public const string WithdrawalErrorMessage = "Withdrawal failed: You attempted to withdrawal ${0}, but your current balance is only ${1}";
        public const string BetErrorMessage = "Bet failed: Either you do not have enough funds or your bet is out of range ${0}-${1}";

        public const string DepositSuccessMessage = "Your deposit of ${0} was successful. Your current balance is: ${1}";
        public const string WithdrawalSuccessMessage = "Your withdrawal of ${0} was successful. Your current balance is: ${1}";
        public const string BetWinMessage = "Congrats - you won ${0}! Your current balance is: ${1}";
        public const string BetLoseMessage = "No luck this time! Your current balance is: ${0}";

    }
}
