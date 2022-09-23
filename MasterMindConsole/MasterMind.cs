using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindConsole
{
    class MasterMind
    {
        //public List<SolutionModel> PossibleSolutions { get; set; }
        //public List<SolutionModel> TippHistory { get; set; }
        public List<PossibilityModel> Possiblities { get; set; }

        public MasterMind()
        {
            //PossibleSolutions = new List<SolutionModel>();
            //TippHistory = new List<SolutionModel>();
            Possiblities = new List<PossibilityModel>();

            IEnumerable<int> baseValues = new List<int>() { 1,2,3,4,5,6 };
            var permutations = GetPermutations<int>(baseValues, 4).ToList();
            foreach (var possibility in permutations)
            {
                var elements = possibility.ToArray();
                SolutionModel solution = new SolutionModel();
                solution.AddValues(elements);
                //PossibleSolutions.Add(solution);

                PossibilityModel possibilityModel = new PossibilityModel();
                possibilityModel.Solution = solution;

                foreach (var possibilityInner in permutations)
                {
                    var elementsInner = possibilityInner.ToArray();
                    SolutionModel solutionInner = new SolutionModel();
                    solutionInner.AddValues(elementsInner);
                    solutionInner.ScoreIt(solution.Values);

                    possibilityModel.PossibleSolutions.Add(solutionInner);
                }

                Possiblities.Add(possibilityModel);
            }
        }

        public void DecreaseSolutions(SolutionModel input)
        {
            foreach (var possiblity in Possiblities)
            {
                if (possiblity.CanBeSolution)
                {
                    var sameElement = possiblity.PossibleSolutions.FirstOrDefault(x => ArraysAreTheSame(x.Values, input.Values));

                    if (ArraysAreTheSame(sameElement.Scores, input.Scores) == false)
                    {
                        possiblity.CalculateDifference(input);
                        possiblity.CanBeSolution = false;
                    }
                }
            }
        }

        public SolutionModel BestTipp()
        {
            var highestDifference = Possiblities.Where(x => x.CanBeSolution).Max(x => x.DifferenceFromTips);
            var result = Possiblities.Where(x => x.CanBeSolution).FirstOrDefault(x => x.DifferenceFromTips == highestDifference);

            return result.Solution;
        }

        IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        bool ArraysAreTheSame(int[] input1, int[] input2)
        {
            bool same = true;
            for (int i = 0; i < input1.Length; i++)
            {
                if(input1[i] != input2[i])
                {
                    same = false;
                    break;
                }
            }
            return same;
        }

    }
}
