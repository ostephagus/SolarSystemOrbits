using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class RegressionCalculator
    {
        private int length; //N and S_0

        #region sumDefinitions
        private double xSum; //S_1
        private double xSquaredsSum; //S_2
        private double ySum; //s_0
        private double xTimesYSum; //s_1
        #endregion


        private double[] xarr;
        private double[] yarr;

        public double[] Xarr { get => xarr; set => xarr = value; }
        public double[] Yarr { get => yarr; set => yarr = value; }

        public RegressionCalculator(double[] xarr, double[] yarr)
        {
            Xarr = xarr;
            Yarr = yarr;
            length = Xarr.Length;
        }

        private static double[] Map(double[] array, Func<double, double> MappingFunction)
        {
            double[] returnArray = new double[array.Length];
            for (int i = 0; i < array.Length; i++) //Iterate through the array
            {
                returnArray[i] = MappingFunction(array[i]); //Apply the function element-wise
            }
            return returnArray;
        }

        private static double[] MapParallelArrays(double[] array1, double[] array2, Func<double, double, double> MappingFunction)
        {
            if (array1.Length != array2.Length)
            {
                throw new ArgumentException("Parallel arrays did not have the same length");
            }

            double[] returnArray = new double[array1.Length];
            for (int i = 0; i < array1.Length; i++)
            {
                returnArray[i] = MappingFunction(array1[i], array2[i]);
            }
            return returnArray;
        }

        private void CalculateSums()
        {
            xSum = xarr.Sum();
            xSquaredsSum = Map(xarr, (x) => x * x).Sum();
            ySum = yarr.Sum();
            xTimesYSum = MapParallelArrays(xarr, yarr, (x, y) => x * y).Sum();
        }

        private double CalculateGradient()
        {
            return (xTimesYSum * length + ySum * -xSum) / (xSquaredsSum * length - xSum * xSum);
        }

        private double calculateYIntercept()
        {
            return (xTimesYSum * -xSum + ySum * xSquaredsSum) / (xSquaredsSum * length - xSum * xSum);
        }

        private double CalculateRSquared(double gradient, double yIntercept) //a, b
        {
            double numeratorSum = 0;
            for (int i = 0; i < length; i++)
            {
                numeratorSum += Math.Pow((gradient * xarr[i] + yIntercept - yarr[i]), 2);
            }

            double denominatorSum = 0;
            for (int i = 0; i < length; i++)
            {
                denominatorSum += Math.Pow((ySum / length - yarr[i]), 2);
            }
            return 1 - numeratorSum / denominatorSum;
        }

        public (double, double, double) Calculate()
        {
            CalculateSums();
            double gradient = CalculateGradient();
            double yIntercept = calculateYIntercept();
            return (gradient, yIntercept, CalculateRSquared(gradient, yIntercept));
        }
    }
}
