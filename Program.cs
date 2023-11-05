using System.CommandLine;
using System.Text;

namespace Alphabet;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var rootCommand = new RootCommand("number of letter game");

        rootCommand.SetHandler(handler => { Challenge(); });

        rootCommand.Invoke(args);
    }

    private static void Challenge()
    {
        // Clear screen
        Console.WriteLine("\x1b[2J");
        Console.WriteLine("Control-c to exit.");

        var shuffledLetters = Enumerable.Range(1, 26)
            .Select(letter => (letter, Random.Shared.Next()))
             .OrderBy(tuple => tuple.Item2)
             .Select(tuple => tuple.letter)
             .ToArray();

        var wins = 0;

        var gameStart = DateTime.UtcNow;

        for (var x = 0; x < shuffledLetters.Length; x++)
        {
            var answer = 'a' - 1 + shuffledLetters[x];

            var guessStart = DateTime.UtcNow;

            // Blue and bold
            Console.Write($"#{(x + 1):00} of {shuffledLetters.Length:00}. Which letter is \x1b[1;34m{shuffledLetters[x],2:##}\x1b[0m: ");

            var input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            var guessTime = DateTime.UtcNow - guessStart;

            // Green
            string isCorrectString = "👍 \x1b[32mCorrect!\x1b[0m";
            if (input.ToString().ToLower().FirstOrDefault() == answer)
            {
                wins++;
            }
            else
            {
                // Red
                isCorrectString = "👺 \x1b[1;31mWrong!\x1b[0m  ";
            }

            //Move up a line and over 33 columns
            Console.WriteLine($"\x1b[F\x1b[33G {isCorrectString} answer: {(char)answer} time: {guessTime.TotalSeconds:0.000}s");
        }

        var gameTime = DateTime.UtcNow - gameStart;
        Console.WriteLine($"✅ Done. {wins}/26 Total time: {gameTime.TotalSeconds:0.0}s");
    }
}
