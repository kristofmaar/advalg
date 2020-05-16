using AdvancedAlgorithms_ISGK7K.Settings;
using AdvancedAlgorithms_ISGK7K.Solvers;

namespace AdvancedAlgorithms_ISGK7K
{
    class Program
    {
        static void Main(string[] args)
        {
            //FA_With_GA faWithGa = new FA_With_GA(new GASettings { 
            //    NumberOfPopulation = 500,
            //    NumberOfParents = 2,
            //    ElitismNumber = 10,
            //    MutationPercent = 5,
            //    MaxIterations = 100,
            //    InputFilePath = "Input/FuncAppr1.txt"
            //});
            //faWithGa.SolveProblem();

            SBPP_With_HC sbppWithHc = new SBPP_With_HC(new HCSettings()
            {
                Epsilon = 10,
                Dimension = 3,
                MaxCoordinates = 400,
                FittnessToReach = 1.1,
                InputFilePath = "Input/Points.txt"
            });
            sbppWithHc.SolveProblem();
        }
    }
}
