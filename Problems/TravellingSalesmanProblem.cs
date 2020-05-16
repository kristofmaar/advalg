using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
    public class TravellingSalesmanProblem
    {
        private Route baseRoute = new Route();

        public Route BaseRoute { get { return baseRoute; } }

        public TravellingSalesmanProblem(string fileName)
        {
            loadTownsFromFile(fileName);
        }

        private void loadTownsFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] lineSplit = line.Split("\t");
                baseRoute.Add(new Town()
                {
                    x = float.Parse(lineSplit[0]),
                    y = float.Parse(lineSplit[1])
                });
            }
        }

        public float objective(Route route)
        {
            float sum_length = 0;

            for (int ti = 0; ti < route.Count - 1; ti++)
            {
                Town t1 = route[ti];
                Town t2 = route[ti + 1];
                sum_length += (float)Math.Sqrt(Math.Pow(t1.x - t2.x, 2) + Math.Pow(t1.y - t2.y, 2));
            }
            return sum_length;
        }
    }

    public class Town
    {
        public float x;
        public float y;
    };

    public class Route : List<Town>
    {
        public double CalculateFitness(TravellingSalesmanProblem tSP)
        {
            return tSP.objective(this);
        }

        public override string ToString()
        {
            string output = string.Empty;
            foreach (Town town in this)
            {
                output += String.Format("({0},{1})\n", town.x, town.y);
            }
            return output;
        }

        public static Route CreateCopy(Route route)
        {
            Route newRoute = new Route();
            foreach (Town town in route)
            {
                newRoute.Add(town);
            }
            return newRoute;
        }
    }
}
