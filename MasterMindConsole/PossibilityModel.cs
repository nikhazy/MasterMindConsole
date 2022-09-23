using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindConsole
{
    class PossibilityModel
    {
        public SolutionModel Solution { get; set; }
        public List<SolutionModel> PossibleSolutions { get; set; }
        public bool CanBeSolution { get; set; } = true;
        public int DifferenceFromTips { get; set; }

        public PossibilityModel()
        {
            PossibleSolutions = new List<SolutionModel>();
        }

        public void CalculateDifference(SolutionModel input)
        {
            int currentDiffer = 0;
            for (int i = 0; i < input.Values.Length; i++)
            {
                if(Solution.Values.Contains(input.Values[i]) == false)
                {
                    currentDiffer += 2;
                }
                else if (input.Values[i] != Solution.Values[i])
                {
                    currentDiffer += 1;
                }
            }
            DifferenceFromTips += currentDiffer;
        }
    }

}
