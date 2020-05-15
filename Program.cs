using AdvancedAlgorithms_ISGK7K.Settings;
using AdvancedAlgorithms_ISGK7K.Solvers;

namespace AdvancedAlgorithms_ISGK7K
{
    class Program
    {
        static void Main(string[] args)
        {
            FA_With_GA teszt = new FA_With_GA(new GASettings { 
                NumberOfPopulation = 200,
                NumberOfParents = 5,
                ElitismNumber = 10,
                MutationPercent = 10,
                MaxIterations = 10000,
                InputFilePath = "Input/FuncAppr1.txt"
            });
            teszt.SolveProblem();
        }
    }
}
