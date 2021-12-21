using KnnClasifier.Algorithms;
using KnnClasifier.Interfaces.Algorithms;
using KnnClasifier.Model;
using System.Collections.Generic;
using System.Linq;

namespace KnnClasifier
{
    public class KnnClasifier
    {
        private readonly double[][] _trainingData;
        private readonly string[] _trainingClasses;

        private int NumberOfTrainingInstances => _trainingData.Length;
        
        private readonly IDistanceCalculator _distanceCalculator;

        public KnnClasifier(double[][] trainingData, string[] trainingClasses)
        {
            _trainingData = trainingData;
            _trainingClasses = trainingClasses;
            _distanceCalculator = new EuclidDistanceCalculator();
        }

        public KnnClasifier(double[][] trainingData, string[] trainingClasses, IDistanceCalculator distanceCalculator) 
            : this ( trainingData, trainingClasses )
        {
            _distanceCalculator = distanceCalculator;
        }

        public string Clasify(double[] unknown, int k)
        {
            var distances = new IndexDistance[NumberOfTrainingInstances];

            for (int i = 0; i < NumberOfTrainingInstances; i++)
            {
                var trainingInstance = _trainingData[i];

                var distance = _distanceCalculator.GetDistance(unknown, trainingInstance);

                distances[i] = new IndexDistance(i, distance);
            }

            var kNearestInstanceIds = distances.OrderBy(x => x.Distance)
                                             .Take(k)
                                             .Select(x => x.Id);


            var relevantClass = GetRelevantClass(kNearestInstanceIds);

            return relevantClass;
        }

        private string GetRelevantClass(IEnumerable<int> kNearestInstanceIds)
        {
            var classes = new Dictionary<string, int>();
            foreach(var id in kNearestInstanceIds)
            {
                var instanceClass = _trainingClasses[id];
                if (!classes.ContainsKey(instanceClass))
                {
                    classes.Add(instanceClass, 0);
                }

                classes[instanceClass] = classes[instanceClass] + 1;
            }

            return classes.OrderByDescending(x => x.Value)
                          .First()
                          .Key;
        }
    }
}
