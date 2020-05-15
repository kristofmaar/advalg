using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
    public class FunctionApproximation
    {
        protected List<ValuePair> known_values = new List<ValuePair>();

        public FunctionApproximation(string fileName)
        {
            loadKnownValuesFromFile(fileName);
        }

        public void loadKnownValuesFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] lineSplit = line.Split("\t");
                known_values.Add(new ValuePair()
                {
                    input = double.Parse(lineSplit[0]),
                    output = double.Parse(lineSplit[1])
                });
            }
        }

        public double objective(Chromosome coefficients)
        {
            double sum_diff = 0;

            foreach (ValuePair valuePair in known_values)
            {
                double x = valuePair.input;
                double y = coefficients[0] * Math.Pow(x - coefficients[1], 3) +
                    coefficients[2] * Math.Pow(x - coefficients[3], 2) +
                    coefficients[4];
                double diff = Math.Pow(y - valuePair.output, 2);
                sum_diff += diff;
            }
            return sum_diff;
        }
    }

    public class ValuePair
    {
        public double input;
        public double output;
    }

    public class Chromosome : List<double>
    {
        public double CalculateFitness(FunctionApproximation functionApproximation) {
            return functionApproximation.objective(this);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4}", this[0], this[1], this[2], this[3], this[4]);
        }
    }
}
