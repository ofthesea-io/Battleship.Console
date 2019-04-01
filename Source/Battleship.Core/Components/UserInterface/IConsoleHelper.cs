namespace Battleship.Core.Components.UserInterface
{
    using System;

    public interface IConsoleHelper
    {
        string HitMessage { get; }

        string InvalidMessage { get; }

        string MissMessage { get; }

        string CompletedMessage { get; }

        string StartGameMessage { get; }

        string ApplicationErrorMessage { get; }

        void ClearLine(int inputLine);

        void Write(string input);

        void WriteLine(string input);

        void ClearBufferToWriteLine(string input);

        string ReadLine();

        void SetColour(ConsoleColor colour);

        string GetUserInput(string userInput);
    }
}