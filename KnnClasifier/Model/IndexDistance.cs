using System;

namespace KnnClasifier.Model
{
    public class IndexDistance
    {
        public int Id { get; set; }
        public double Distance { get; set; }

        public IndexDistance(int id, double distance)
        {
            Id = id;
            Distance = distance;
        }
    }
}
