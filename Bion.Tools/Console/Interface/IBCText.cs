using System;

namespace Bion.Tools
{
    public interface IBCText
    {
        ConsoleColor? BackgroundColor { get; }
        int BUID { get; }
        bool IsNewLine { get; }
        bool IsOnlyForStalkers { get; }
        string Text { get; }
        ConsoleColor? TextColor { get; }

        string ToString();
    }
}