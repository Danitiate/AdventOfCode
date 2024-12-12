using AdventOfCode.Core.Models;

namespace AdventOfCode2024.Solutions
{
    public class SolutionSelectorService : ISolutionSelectorService
    {
        public List<ISolution> GetRequestedSolutions(int selectedDay)
        {
            var solutions = new List<ISolution>();
            switch (selectedDay)
            {
                case 1: solutions.AddRange([new Day_1.SolutionA(), new Day_1.SolutionB()]); break;
                //case 2: solutions.AddRange([new Day_2.SolutionA(), new Day_2.SolutionB()]); break;
                //case 3: solutions.AddRange([new Day_3.SolutionA(), new Day_3.SolutionB()]); break;
                //case 4: solutions.AddRange([new Day_4.SolutionA(), new Day_4.SolutionB()]); break;
                //case 5: solutions.AddRange([new Day_5.SolutionA(), new Day_5.SolutionB()]); break;
                //case 6: solutions.AddRange([new Day_6.SolutionA(), new Day_6.SolutionB()]); break;
                //case 7: solutions.AddRange([new Day_7.SolutionA(), new Day_7.SolutionB()]); break;
                //case 8: solutions.AddRange([new Day_8.SolutionA(), new Day_8.SolutionB()]); break;
                //case 9: solutions.AddRange([new Day_9.SolutionA(), new Day_9.SolutionB()]); break;
                //case 10: solutions.AddRange([new Day_10.SolutionA(), new Day_10.SolutionB()]); break;
                //case 11: solutions.AddRange([new Day_11.SolutionA(), new Day_11.SolutionB()]); break;
                //case 12: solutions.AddRange([new Day_12.SolutionA(), new Day_12.SolutionB()]); break;
                //case 13: solutions.AddRange([new Day_13.SolutionA(), new Day_13.SolutionB()]); break;
                //case 14: solutions.AddRange([new Day_14.SolutionA(), new Day_14.SolutionB()]); break;
                //case 15: solutions.AddRange([new Day_15.SolutionA(), new Day_15.SolutionB()]); break;
                //case 16: solutions.AddRange([new Day_16.SolutionA(), new Day_16.SolutionB()]); break;
                //case 17: solutions.AddRange([new Day_17.SolutionA(), new Day_17.SolutionB()]); break;
                //case 18: solutions.AddRange([new Day_18.SolutionA(), new Day_18.SolutionB()]); break;
                //case 19: solutions.AddRange([new Day_19.SolutionA(), new Day_19.SolutionB()]); break;
                //case 20: solutions.AddRange([new Day_20.SolutionA(), new Day_20.SolutionB()]); break;
                //case 21: solutions.AddRange([new Day_21.SolutionA(), new Day_21.SolutionB()]); break;
                //case 22: solutions.AddRange([new Day_22.SolutionA(), new Day_22.SolutionB()]); break;
                //case 23: solutions.AddRange([new Day_23.SolutionA(), new Day_23.SolutionB()]); break;
                //case 24: solutions.AddRange([new Day_24.SolutionA(), new Day_24.SolutionB()]); break;
                //case 25: solutions.AddRange([new Day_25.SolutionA(), new Day_25.SolutionB()]); break;
            }

            return solutions;
        }
    }
}
