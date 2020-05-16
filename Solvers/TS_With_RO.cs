using AdvancedAlgorithms_ISGK7K.Problems;
using AdvancedAlgorithms_ISGK7K.Settings;
using MathNet.Numerics.Distributions;
using System;

namespace AdvancedAlgorithms_ISGK7K.Solvers
{
    /// <summary>
    /// Travelling Salesman Problem solved with Random Optimization
    /// </summary>
    public class TS_With_RO
    {
        Normal normal = new Normal();
        ROSettings settings;
        TravellingSalesmanProblem tSP;

        public TS_With_RO(ROSettings settings)
        {
            this.settings = settings;
            tSP = new TravellingSalesmanProblem(settings.InputFilePath);
        }

        public void SolveProblem()
        {
            Route route = tSP.BaseRoute;
            while (route.CalculateFitness(tSP) > settings.FittnessToReach) // Stop condition
            {
                Route newRoute = GenerateNewRoute(route);
                if (newRoute.CalculateFitness(tSP) < route.CalculateFitness(tSP))
                {
                    route = newRoute;
                    Console.WriteLine(String.Format("Found better solution. Fittness: {0}", newRoute.CalculateFitness(tSP)));
                }

            }
            Console.WriteLine(String.Format("Found best solution. Fittness: {0}, coordinates: {1}", route.CalculateFitness(tSP), route.ToString()));
        }

        private Route GenerateNewRoute(Route route)
        {
            Route newRoute = Route.CreateCopy(route);
            MixRouteWithNormalDistribution(newRoute);
            return newRoute;
        }

        private void MixRouteWithNormalDistribution(Route route)
        {
            int n = route.Count;
            while (n > 1)
            {
                n--;
                int shift = (int)Normal.Sample(settings.Mean, settings.Variance);
                int randomIndex = n + shift;
                if (randomIndex < 0) randomIndex = 0;
                if (randomIndex >= route.Count) randomIndex = route.Count - 1;
                Town value = route[randomIndex];
                route[randomIndex] = route[n];
                route[n] = value;
            }
        }
    }
}
