# 🎰 BettyGame

**BettyGame** is a lightweight console-based betting game built with C#. It allows users to manage a virtual wallet by placing bets, depositing funds, and making withdrawals. The game outcomes are determined using configurable probabilities.

---

## 💻 Commands

| Command             | Description                                                   | Example        |
|---------------------|---------------------------------------------------------------|----------------|
| `deposit <amount>`  | Adds funds to your wallet                                     | `deposit 100`  |
| `withdrawal <amount>` | Withdraws funds from your wallet if the balance is sufficient | `withdrawal 20`|
| `bet <amount>`      | Places a bet and processes win/loss based on config settings  | `bet 10`       |
| `exit`              | Exits the game                                                | `exit`         |

---

## ⚙️ Configuration (`appsettings.json`)

The game's logic and outcomes are driven by the following configuration:

```json
"BetOptions": {
  "SmallestBet": 1.00,
  "BiggestBet": 10.00,
  "Loss": {
    "Chance": 0.5
  },
  "SmallWin": {
    "Chance": 0.4,
    "WinRatioStart": 1.01,
    "WinRatioEnd": 2.00
  },
  "BigWin": {
    "Chance": 0.1,
    "WinRatioStart": 2.01,
    "WinRatioEnd": 10.00
  }
}

## 📝 Notes:

* The sum of Loss.Chance, SmallWin.Chance, and BigWin.Chance should equal 1.0.
* WinRatioStart and WinRatioEnd determine the multiplier for winnings.