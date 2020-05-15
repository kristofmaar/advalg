using AdvancedAlgorithms_ISGK7K.Problems;
using AdvancedAlgorithms_ISGK7K.Settings;
using System;

namespace AdvancedAlgorithms_ISGK7K.Solvers
{
    /// <summary>
    /// Smallest Boundary Polygon Problem solved with Hill Climbing Stochastic
    /// </summary>
    public class SBPP_With_HC
    {
        static Random random = new Random();
        HCSettings settings;
        SmallestBoundaryPolygonProblem sBPP;

        public SBPP_With_HC(HCSettings settings)
        {
            this.settings = settings;
            sBPP = new SmallestBoundaryPolygonProblem(settings.InputFilePath);
        }

        public Polygon SolveProblem()
        {
            Polygon polygon = GenerateRandomPolygon();
            for (int i = 0; i < settings.MaxIterations; i++)
            {
                Polygon newPolygon = GenerateRandomPolygon();
                if(newPolygon.CalculateFitness(sBPP) < polygon.CalculateFitness(sBPP))
                {
                    polygon = newPolygon;
                    Console.WriteLine(newPolygon.CalculateFitness(sBPP));
                }
            }
            Console.WriteLine(String.Format("Found solution after {0} iterations. Fitness: {1}, coordinates: {2}", settings.MaxIterations, polygon.CalculateFitness(sBPP), polygon.ToString()));
            return polygon;
        }

        private Polygon GenerateRandomPolygon()
        {
            Polygon polygon = new Polygon();
            for (int i = 0; i < settings.Dimension; i++)
            {
                polygon.Add(new Point() { x = random.Next(0,400), y = random.Next(0,400) });
            }
            return polygon;
        }
    }
}
