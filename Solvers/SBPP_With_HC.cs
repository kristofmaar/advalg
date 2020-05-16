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
            Polygon polygon = InitalizePolygon();
            while (polygon.CalculateFitness(sBPP) > settings.FittnessToReach) // Stop condition
            {
                Polygon newPolygon = MovePolygonRandomly(polygon);
                if(newPolygon.CalculateFitness(sBPP) < polygon.CalculateFitness(sBPP))
                {
                    polygon = newPolygon;
                    Console.WriteLine(String.Format("Found better solution. Fittness: {0}, coordinates: {1}", polygon.CalculateFitness(sBPP), polygon.ToString()));
                }
            }
            Console.WriteLine(String.Format("Found best solution. Fittness: {0}, coordinates: {1}", polygon.CalculateFitness(sBPP), polygon.ToString()));
            return polygon;
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

        private Polygon MovePolygonRandomly(Polygon polygon)
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
