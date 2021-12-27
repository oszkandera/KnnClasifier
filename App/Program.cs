using System;
using System.Collections.Generic;
using System.Globalization;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"..\..\..\Data\iris.data";

            var dataLoader = new CsvDataLoader();
            var data = dataLoader.LoadData(filePath);

            var extractedTrainingData = ExtractTrainingData(data, 0.6);

            var (trainingData, trainingClasses) = PrepareTrainingData(extractedTrainingData);

            var clasifier = new KnnClasifier.KnnClasifier(trainingData, trainingClasses);

            var unknownInstance = new double[] { 5.4, 3.0, 4.5, 1.5 };
            var k = 5;

            var classification = clasifier.Clasify(unknownInstance, k);
        }

        private static List<List<string>> ExtractTrainingData(List<List<string>> data, double requiredTrainingDataSize)
        {
            var random = new Random();

            var registeredInstances = new HashSet<int>() { -1 };

            var trainingData = new List<List<string>>();

            for (int i = 0; i < data.Count * requiredTrainingDataSize; i++)
            {
                var instanceId = -1;
                while (registeredInstances.Contains(instanceId))
                {
                    instanceId = random.Next(0, data.Count - 1);
                }

                trainingData.Add(data[instanceId]);
            }

            return trainingData;
        }

        private static (double[][], string[]) PrepareTrainingData(List<List<string>> data)
        {
            var valueData = new double[data.Count][];
            var classes = new string[data.Count];

            for (int instance = 0; instance < data.Count; instance++)
            {
                classes[instance] = data[instance][^1];

                var numberOfValueAttributes = data[instance].Count - 1;
                valueData[instance] = new double[numberOfValueAttributes];

                for (int attribute = 0; attribute < numberOfValueAttributes; attribute++)
                {
                    valueData[instance][attribute] = double.Parse(data[instance][attribute], CultureInfo.InvariantCulture);
                }
            }

            return (valueData, classes);
        }
    }
}
