using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
	class SmallestBoundaryPolygonProblem
    {
		public List<Point> points = new List<Point>();

		public float distanceFromLine(Point lp1, Point lp2, Point p)
		{
			return (float)(((lp2.y - lp1.y) * p.x - (lp2.x - lp1.x) * p.y + lp2.x * lp1.y - lp2.y * lp1.x) / Math.Sqrt(Math.Pow(lp2.y - lp1.y, 2) + Math.Pow(lp2.x - lp1.x, 2)));
		}
		
		public float lengthOfBoundary(List<Point> solution)
		{
			float sum_length = 0;

			for (int li = 0; li < solution.Count - 1; li++)
			{
				Point p1 = solution[li];
				Point p2 = solution[(li + 1) % solution.Count];
				sum_length += (float)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
			}
			return sum_length;
		}

		public float outerDistanceToBoundary(List<Point> solution)
		{
			float sum_min_distances = 0;

			for (int pi = 0; pi < points.Count; pi++)
			{
				float min_dist = 0;
				for (int li = 0; li < solution.Count; li++)
				{
					float act_dist = distanceFromLine(solution[li], solution[(li + 1) % solution.Count], points[pi]);
					if (li == 0 || act_dist < min_dist)
						min_dist = act_dist;
				}
				if (min_dist < 0)
					sum_min_distances += -min_dist;
			}
			return sum_min_distances;
		}

		public float objective(List<Point> solution)
		{
			return lengthOfBoundary(solution);
		}

		public float constraint(List<Point> solution)
		{
			return -outerDistanceToBoundary(solution);
		}

		public void loadPointsFromFile(string fileName)
		{
			string[] lines = File.ReadAllLines(fileName);

			foreach (string line in lines)
			{
				string[] lineSplit = line.Split("\t");
				points.Add(new Point()
				{
					x = float.Parse(lineSplit[0]),
					y = float.Parse(lineSplit[1])
				});
			}
		}

		public void savePointsToFile(string fileName, List<Point> pointVector)
		{
			List<string> output = new List<string>();
			foreach (Point point in pointVector)
			{
				output.Add(String.Format("{0}\t{1}", point.x, point.y));
			}
			File.WriteAllLines(fileName, output);
		}
	}

	public class Point
	{
		public float x;
		public float y;
	}
}
