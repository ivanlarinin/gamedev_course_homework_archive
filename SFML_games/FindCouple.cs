using System;
using System.Threading;

class Program
{
    static string[,] colorNames = new string[,]
    {
        { "Red", "Green", "Blue", "Cyan" }
    };

    static ConsoleColor[] consoleColors = new ConsoleColor[]
    {
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Blue,
        ConsoleColor.Cyan
    };

    static char[] inputKeys = new char[] { 'q', 'w', 'e', 'r' };

    static int score = 0;
    static bool playing = true;
    static Random rand = new Random();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== COLOR MATCH GAME ===");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose option: ");
            var choice = Console.ReadKey().Key;

            if (choice == ConsoleKey.D1)
            {
                StartGame();
            }
            else if (choice == ConsoleKey.D2)
            {
                break;
            }
        }
    }

    static void StartGame()
    {
        score = 0;
        playing = true;
        int rounds = 10;

        for (int i = 0; i < rounds; i++)
        {
            Console.Clear();
            int textIndex = rand.Next(4);
            int colorIndex = rand.Next(4);

            string text = colorNames[0, textIndex];
            ConsoleColor displayColor = consoleColors[colorIndex];

            Console.ForegroundColor = displayColor;
            Console.WriteLine($"\nRound {i + 1}/{rounds}: What is the color of this word?");
            Console.WriteLine($"\n\t{text}\n");
            Console.ResetColor();

            Console.WriteLine("q - Red, w - Green, e - Blue, r - Cyan");
            Console.Write("Your answer: ");

            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            int selectedColorIndex = Array.IndexOf(inputKeys, Char.ToLower(input));

            if (selectedColorIndex == colorIndex)
            {
                Console.WriteLine("✅ Correct!");
                score++;
            }
            else
            {
                Console.WriteLine($"❌ Wrong! Correct was: {colorNames[0, colorIndex]}");
            }

            Thread.Sleep(1000);
        }

        Console.WriteLine($"\n🎯 Game Over! Your score: {score}/{rounds}");
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }
}
