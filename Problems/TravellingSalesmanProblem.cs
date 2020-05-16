using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
    public class TravellingSalesmanProblem
    {
        public List<Town> towns;

        public void loadTownsFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] lineSplit = line.Split("\t");
                towns.Add(new Town()
                {
                    x = float.Parse(lineSplit[0]),
                    y = float.Parse(lineSplit[1])
                });
            }
        }

        public float objective(List<Town> route)
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
}
