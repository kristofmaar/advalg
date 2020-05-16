using AdvancedAlgorithms_ISGK7K.Settings;
using AdvancedAlgorithms_ISGK7K.Solvers;
using System;
using System.Diagnostics;

namespace AdvancedAlgorithms_ISGK7K
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Function Approximation solved with Genetic Algorithm");
            Console.WriteLine("2) Smallest Boundary Polygon Problem solved with Hill Climbing Stochastic");
            Console.WriteLine("3) Travelling Salesman Problem solved with Random Optimization");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");

            var input = Console.ReadKey();
            Console.WriteLine();
            Stopwatch sw = Stopwatch.StartNew();

            switch (input.Key)
            {
                case ConsoleKey.D1:
                    FA_With_GA faWithGa = new FA_With_GA(new GASettings
                    {
                        NumberOfPopulation = 500,
                        NumberOfParents = 2,
                        ElitismNumber = 10,
                        MutationPercent = 5,
                        FitnessToReach = 1,
                        InputFilePath = "Input/FuncAppr1.txt"
                    });
                    faWithGa.SolveProblem();
                    break;
                case ConsoleKey.D2:
                    SBPP_With_HC sbppWithHc = new SBPP_With_HC(new HCSettings()
                    {
                        Epsilon = 10,
                        Dimension = 3,
                        MaxCoordinates = 400,
                        FitnessToReach = 1.1,
                        InputFilePath = "Input/Points.txt"
                    });
                    sbppWithHc.SolveProblem();
                    break;
                case ConsoleKey.D3:
                    TS_With_RO tsWithRo = new TS_With_RO(new ROSettings()
                    {
                        FitnessToReach = 4500,
                        Mean = 0,
                        Variance = 2,
                        InputFilePath = "Input/Towns.txt"
                    });
                    tsWithRo.SolveProblem();
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Okay, bye!");
                    break;
                default:
                    break;
            }
            sw.Stop();
            Console.WriteLine(String.Format("\nTime elapsed: {0}", sw.Elapsed.ToString()));
        }
    }
}
