using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedAlgorithms_ISGK7K.Problems
{
    public class FunctionApproximation
    {
        protected List<ValuePair> known_values;

        public void loadKnownValuesFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] lineSplit = line.Split("\t");
                known_values.Add(new ValuePair()
                {
                    input = float.Parse(lineSplit[0]),
                    output = float.Parse(lineSplit[1])
                });
            }
        }

        public float objective(List<float> coefficients)
        {
            float sum_diff = 0;

            foreach (ValuePair valuePair in known_values)
            {
                float x = valuePair.input;
                float y = (float)(
                    coefficients[0] * Math.Pow(x - coefficients[1], 3) +
                    coefficients[2] * Math.Pow(x - coefficients[3], 2) +
                    coefficients[4]);
                float diff = (float)Math.Pow(y - valuePair.output, 2);
                sum_diff += diff;
            }
            return sum_diff;
        }
    }

    public class ValuePair
    {
        public float input;
        public float output;
    };
}
