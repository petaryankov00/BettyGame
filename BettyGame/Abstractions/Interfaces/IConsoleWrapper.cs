﻿namespace BettyGame.Abstractions.Interfaces
{
    public interface IConsoleWrapper
    {
        void WriteLine(string message);
        string? ReadLine();
    }
}
