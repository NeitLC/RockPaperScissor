using System.Security.Cryptography;

namespace RockPaperScissor;

public static class Program
{
    private static bool CheckValidArgs(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            Console.WriteLine("Invalid arguments: pass odd number of moves and (3 or more)");
            return false;
        }

        if (args.Length != args.Distinct().Count())
        {
            Console.WriteLine("Invalid arguments: all moves must be distinct.");
            return false;
        }

        return true;
    }

    public static void Main(string[] args)
    {
        if (!CheckValidArgs(args))
        {
            return;
        }
        
        var generator = new Generator(); 
        var decision = new Result(args.Length);
        var table    = new Table(args.ToList());
        
        bool gameFinished = false;
        
        while (!gameFinished)
        {
            var key = generator.GenerateRandomKey();
            var compMove = RandomNumberGenerator.GetInt32(args.Length);
            var hmac = generator.GenerateHmac(key, args[compMove]);
            
            Console.WriteLine($"HMAC: {hmac}");
            Console.WriteLine("Available moves: ");
            
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"{(i + 1)} - {args[i]}");
            }
            
            Console.WriteLine("0 - Exit");
            Console.WriteLine("? - Help");
            
            Console.Write("Enter your move: ");
            var move = Console.ReadLine();
            
            if (move == "0")
            {
                gameFinished = true;
                continue;
            }
            
            if (move == "?")
            {
                table.PrintTable();
                Console.Write("\n");
                continue;
            }

            if (!int.TryParse(move, out var playerMove) 
                || playerMove <= 0 
                || playerMove > args.Length)
            {
                Console.Write("\n");
                continue;
            }
            
            Console.WriteLine($"Your move: {args[playerMove - 1]}");
            Console.WriteLine($"Computer move: {args[compMove]}");
            
            switch (decision.Decide(playerMove - 1, compMove))
            {
                case Value.Win:
                    Console.WriteLine("You win!");
                    break;
                case Value.Lose:
                    Console.WriteLine("You lose!");
                    break;
                case Value.Draw:
                    Console.WriteLine("It's a draw!");
                    break;
            }
            
            Console.WriteLine($"HMAC key {key}");
            Console.WriteLine("\n");
        }
    }
}