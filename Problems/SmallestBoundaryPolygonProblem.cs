using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
	public class SmallestBoundaryPolygonProblem
    {
		private List<Point> points = new List<Point>();

		public SmallestBoundaryPolygonProblem(string fileName)
		{
			loadPointsFromFile(fileName);
		}

		public double distanceFromLine(Point lp1, Point lp2, Point p)
		{
			return (((lp2.y - lp1.y) * p.x - (lp2.x - lp1.x) * p.y + lp2.x * lp1.y - lp2.y * lp1.x) / Math.Sqrt(Math.Pow(lp2.y - lp1.y, 2) + Math.Pow(lp2.x - lp1.x, 2)));
		}
		
		public double lengthOfBoundary(List<Point> solution)
		{
			double sum_length = 0;

			for (int li = 0; li < solution.Count - 1; li++)
			{
				Point p1 = solution[li];
				Point p2 = solution[(li + 1) % solution.Count];
				sum_length += Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
			}
			return sum_length;
		}

		public double outerDistanceToBoundary(List<Point> solution)
		{
			double sum_min_distances = 0;

			for (int pi = 0; pi < points.Count; pi++)
			{
				double min_dist = 0;
				for (int li = 0; li < solution.Count; li++)
				{
					double act_dist = distanceFromLine(solution[li], solution[(li + 1) % solution.Count], points[pi]);
					if (li == 0 || act_dist < min_dist)
						min_dist = act_dist;
				}
				if (min_dist < 0)
					sum_min_distances += -min_dist;
			}
			return sum_min_distances;
		}

		//public bool checkIsInBoundary(Polygon polygon)
		//{
		//	bool isInBoundary = true;
		//	foreach (Point point in points)
		//	{
		//		if(!IsInPolygon(polygon, point))
		//		{
		//			isInBoundary = false;
		//			break;
		//		}
		//	}
		//	return isInBoundary;
		//}

		//public static bool IsInPolygon(Polygon polygon, Point testPoint)
		//{
		//	bool result = false;
		//	int j = polygon.Count() - 1;
		//	for (int i = 0; i < polygon.Count(); i++)
		//	{
		//		if (polygon[i].y < testPoint.y && polygon[j].y >= testPoint.y || polygon[j].y < testPoint.y && polygon[i].y >= testPoint.y)
		//		{
		//			if (polygon[i].x + (testPoint.y - polygon[i].y) / (polygon[j].y - polygon[i].y) * (polygon[j].x - polygon[i].x) < testPoint.x)
		//			{
		//				result = !result;
		//			}
		//		}
		//		j = i;
		//	}
		//	return result;
		//}

		public double objective(List<Point> solution)
		{
			return lengthOfBoundary(solution);
		}

		public double constraint(List<Point> solution)
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
					x = double.Parse(lineSplit[0]),
					y = double.Parse(lineSplit[1])
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
		public double x;
		public double y;
	}

	public class Polygon : List<Point>
	{
		public double CalculateFitness(SmallestBoundaryPolygonProblem smallestBoundaryPolygonProblem)
		{
			double calculatedFitness = 0;
			if (smallestBoundaryPolygonProblem.outerDistanceToBoundary(this) != 0)
			{
				calculatedFitness = smallestBoundaryPolygonProblem.objective(this) * smallestBoundaryPolygonProblem.outerDistanceToBoundary(this);
			}
			else
			{
				calculatedFitness = 10000000;
			}
			return calculatedFitness;
		}

		public override string ToString()
		{
			string output = string.Empty;
			foreach (Point point in this)
			{
				output += String.Format(" ({0},{1}) ", point.x, point.y);
			}
			return output;
		}
	}
}
