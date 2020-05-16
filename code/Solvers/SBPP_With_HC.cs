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

        public void SolveProblem()
        {
            Polygon polygon = InitalizePolygon();
            while (polygon.CalculateFitness(sBPP) > settings.FitnessToReach) // Stop condition
            {
                Polygon newPolygon = GenerateRandomPolygon(polygon);
                if(newPolygon.CalculateFitness(sBPP) < polygon.CalculateFitness(sBPP))
                {
                    polygon = newPolygon;
                    Console.WriteLine(String.Format("Found a better solution. Fitness: {0}, coordinates: {1}", polygon.CalculateFitness(sBPP), polygon.ToString()));
                }
            }
            Console.WriteLine(String.Format("Found best solution. Fitness: {0}, coordinates: {1}", polygon.CalculateFitness(sBPP), polygon.ToString()));
        }

        private Polygon InitalizePolygon()
        {
            Polygon polygon = new Polygon();
            for (int i = 0; i < settings.Dimension; i++)
            {
                polygon.Add(new Point() { x = random.Next(0, settings.MaxCoordinates), y = random.Next(0, settings.MaxCoordinates)});
            }
            return polygon;
        }

        private Polygon GenerateRandomPolygon(Polygon polygon)
        {
            Polygon newPolygon = new Polygon();
            foreach (Point point in polygon)
            {
                Point newPoint = new Point()
                {
                    x = point.x + random.Next(-1 * settings.Epsilon, settings.Epsilon),
                    y = point.y + random.Next(-1 * settings.Epsilon, settings.Epsilon)
            };
                newPolygon.Add(newPoint);
            }
            return newPolygon;
        }
    }
}
