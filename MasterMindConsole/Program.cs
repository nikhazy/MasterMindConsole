using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputersGame(1000);
        }
        private static void ComputersGame(int repetitionNumber)
        {
            List<int> results = new List<int>();
            for (int i = 0; i < repetitionNumber; i++)
            {
                MasterMind assist = new MasterMind();

                var solution = CreateRandomSolution();

                int counter = 0;

                while (true)
                {
                    counter++;
                    var tipp = assist.BestTipp();

                    tipp.ScoreIt(solution.Values);

                    //If the winner is found
                    if (tipp.Scores.Where(x => x == 2).Count() == 4)
                    {
                        break;
                    }

                    assist.DecreaseSolutions(tipp);
                }
                Console.WriteLine($"Solution found in {counter} tipps.");
                results.Add(counter);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"******************************************************");
            Console.WriteLine($"Average tipp number: {Math.Round(results.Average(),2)}");
            Console.WriteLine($"Max tipp number: {results.Max()}");
            Console.WriteLine($"Min tipp number: {results.Min()}");
            Console.ReadLine();
        }

        private static SolutionModel CreateRandomSolution()
        {
            Random rnd = new Random();
            int[] resultValues = new int[4];
            for (int j = 0; j < resultValues.Length; j++)
            {
                while (true)
                {
                    int newValue = rnd.Next(1, 6);
                    if (resultValues.Contains(newValue) == false)
                    {
                        resultValues[j] = newValue;
                        break;
                    }
                }
            }
            SolutionModel result = new SolutionModel();
            result.AddValues(resultValues);

            return result;
        }

        private static void PlayersGame()
        {
            MasterMind assist = new MasterMind();

            Random rnd = new Random();
            int[] resultValues = new int[4];
            for (int i = 0; i < resultValues.Length; i++)
            {
                while (true)
                {
                    int newValue = rnd.Next(1, 6);
                    if (resultValues.Contains(newValue) == false)
                    {
                        resultValues[i] = newValue;
                        break;
                    }
                }
            }
            SolutionModel result = new SolutionModel();
            result.AddValues(resultValues);
            Console.WriteLine("Kitaláltam egyet. Tippelhetsz:");

            int counter = 0;
            while (true)
            {
                counter++;
                Console.WriteLine($"#{counter}. Tipped:");
                Console.ForegroundColor = ConsoleColor.Green;
                string line = Console.ReadLine();
                Console.ResetColor();
                if (line == "solution")
                {
                    Console.WriteLine($"*Megoldás*: {result.Values[0]},{result.Values[1]},{result.Values[2]},{result.Values[3]}");
                    continue;
                }
                if (line == "hint")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var possibility in assist.Possiblities)
                    {
                        if (possibility.CanBeSolution)
                        {
                            Console.WriteLine($"*Lehetőség*: {possibility.Solution.Values[0]},{possibility.Solution.Values[1]},{possibility.Solution.Values[2]},{possibility.Solution.Values[3]}");
                        }
                    }
                    Console.ResetColor();
                    continue;
                }
                SolutionModel tipp = new SolutionModel();
                if (tipp.AddValues(line))
                {
                    tipp.ScoreIt(result.Values);
                    if (tipp.Scores.Where(x => x == 2).Count() == 4)
                    {
                        break;
                    }
                    assist.DecreaseSolutions(tipp);
                    Console.WriteLine($"Válasz: {tipp.Scores[0]},{tipp.Scores[1]},{tipp.Scores[2]},{tipp.Scores[3]} (Lehetséges megoldások: {assist.Possiblities.Where(x => x.CanBeSolution).Count()}, Legjobb tipp: {assist.BestTipp().ValuesToString()})");
                }
                else
                {
                    Console.WriteLine("Nem érvényes formátum!");
                }
            }

            Console.WriteLine($"Nyertél!!!!");
            Console.ReadLine();
        }
    }
}
