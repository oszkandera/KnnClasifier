using KnnClasifier.Interfaces.Algorithms;
using System;

namespace KnnClasifier.Algorithms
{
    public class EuclidDistanceCalculator : IDistanceCalculator
    {
        public double GetDistance(double[] instance1, double[] instance2)
        {
            if(instance1.Length != instance2.Length)
            {
                throw new ArgumentException("Inconsistent number of values in instances");
            }

            double distance = 0.0;

            for (int i = 0; i < instance1.Length; i++)
            {
                double temp = instance1[i] - instance2[i];
                distance += temp * temp;
            }

            return Math.Sqrt(distance);
        }
    }
}
