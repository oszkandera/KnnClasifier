namespace KnnClasifier.Interfaces.Algorithms
{
    public interface IDistanceCalculator
    {
        double GetDistance(double[] instance1, double[] instance2);
    }
}
