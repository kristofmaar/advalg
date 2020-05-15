using AdvancedAlgorithms_ISGK7K.Settings;
using AdvancedAlgorithms_ISGK7K.Solvers;

namespace AdvancedAlgorithms_ISGK7K
{
    class Program
    {
        static void Main(string[] args)
        {
            FA_With_GA faWithGa = new FA_With_GA(new GASettings { 
                NumberOfPopulation = 500,
                NumberOfParents = 2,
                ElitismNumber = 10,
                MutationPercent = 5,
                MaxIterations = 10000,
                InputFilePath = "Input/FuncAppr1.txt"
            });
            faWithGa.SolveProblem();
        }
    }
}
