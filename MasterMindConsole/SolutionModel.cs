using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindConsole
{
    class SolutionModel
    {
        public int[] Values { get; set; }
        public int[] Scores { get; set; }
        public SolutionModel()
        {
            Values = new int[4];
            Scores = new int[4];
        }
        public bool AddValues(int[] values)
        {
            Values = values;
            return true;
        }
        public string ValuesToString()
        {
            return $"{Values[0]},{Values[1]},{Values[2]},{Values[3]}";
        }
        public bool AddValues(string line)
        {
            try
            {
                List<int> numbers = new List<int>();
                numbers.Add(Int32.Parse(line[0].ToString()));
                numbers.Add(Int32.Parse(line[1].ToString()));
                numbers.Add(Int32.Parse(line[2].ToString()));
                numbers.Add(Int32.Parse(line[3].ToString()));

                if(line.Length > 4)
                {
                    return false;
                }

                foreach (var item in numbers)
                {
                    if(numbers.Where(x=>x==item).Count() != 1)
                    {
                        return false;
                    }
                    else if(item > 6 || item < 1)
                    {
                        return false;
                    }
                }

                Values = numbers.ToArray();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ScoreIt(int[] solutionValues)
        {
            for (int i = 0; i < solutionValues.Length; i++)
            {
                if(Values[i] == solutionValues[i])
                {
                    Scores[i] = 2;
                }
                else if (solutionValues.Contains(Values[i]))
                {
                    Scores[i] = 1;
                }
            }
            Scores = Scores.ToList().OrderBy(x => x).ToArray();
        }
    }
}
