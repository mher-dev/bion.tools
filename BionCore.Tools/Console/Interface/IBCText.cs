using System;

namespace BionCore.Tools
{
    public interface IBCText
    {
        ConsoleColor? BackgroundColor { get; }
        string BUID { get; }
        bool IsNewLine { get; }
        bool IsOnlyForStalkers { get; }
        string Text { get; }
        ConsoleColor? TextColor { get; }

        string ToString();
    }
}