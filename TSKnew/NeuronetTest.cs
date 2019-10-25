using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSet = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<double, System.Collections.Generic.List<double>>>;

namespace TSKnew
{
    public class NeuronetTest
    {
        public void Start()
        {
            var path = @"C:\Users\Олег\Desktop\testData.txt";
            var data = LoadDataSet(File.ReadAllLines(path));
            var data2 = LoadDataSet2(File.ReadAllLines(path));
            int n = data2[data2.Keys.ElementAt(0)].ElementAt(0).Length;
            var nn = new TSKnew.neuronet.TSKNeuronet(n, data2, m: 3);

            (LearningSet learningSet, LearningSet testSet) = GetTestSet(data, learningSetSizePercent: 0.75);

            nn.Learning(learningSet, iterationCount: 25);
            var errors = nn.Test(testSet);
        }



        private (LearningSet, LearningSet) GetTestSet(LearningSet xSet, double learningSetSizePercent)
        {
            int learningSetSize = (int)(xSet.Count * learningSetSizePercent);
            var newA = new KeyValuePair<double, List<double>>[xSet.Count - learningSetSize];
            Array.Copy(xSet.ToArray(), learningSetSize, newA, 0, xSet.Count - learningSetSize);
            var testSet = newA.ToList();

            var newB = new KeyValuePair<double, List<double>>[learningSetSize];
            Array.Copy(xSet.ToArray(), 0, newB, 0, learningSetSize);
            var learningSet = newB.ToList();
            return (learningSet, testSet);
        }

        public List<KeyValuePair<double, List<double>>> LoadDataSet(string[] stringListDataSet)
        {
            var dataSet = new List<KeyValuePair<double, List<double>>>();

            for (int i = 0; i < stringListDataSet.Length; i++)
            {
                var str = stringListDataSet[i].Split(',');
                var values = str.Select(t1 => double.Parse(t1.Replace('.', ','))).ToArray();
                var xSet = new double[values.Length - 1];
                Array.Copy(values, 0, xSet, 0, values.Length - 1);
                var learningSet = new KeyValuePair<double, List<double>>(values.Last(), xSet.ToList());
                dataSet.Add(learningSet);
            }
            return dataSet;
        }
        //Старое
        public Dictionary<double, List<double[]>> LoadDataSet2(string[] stringListDataSet)
        {
            if (stringListDataSet != null)
            {

                var dataSetDict = new Dictionary<double, List<double[]>>();
                foreach (var item in stringListDataSet)
                {
                    try
                    {
                        var doubleLine = new List<double>();

                        foreach (var strItem in item.Split(','))
                        {
                            if (double.TryParse(strItem.Replace('.', ','), out double val))
                            {
                                doubleLine.Add(val);
                            }
                        }

                        if (!dataSetDict.ContainsKey(doubleLine.Last()))
                            dataSetDict.Add(doubleLine.Last(), new List<double[]>());
                        dataSetDict[doubleLine.Last()].Add(doubleLine.GetRange(0, doubleLine.Count - 1).ToArray());
                    }
                    catch (Exception e)
                    {
                        int a = 12;
                    }
                }

                return dataSetDict;
            }
            return null;
        }
    }
}
