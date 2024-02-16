using AdventOfCode2023.Models;
using System.Collections.Generic;

namespace AdventOfCode2023.Services
{
    public class SolutionSelectorService
    {
        public static List<ISolution> GetRequestedSolutions(string userInput)
        {
            var solutions = new List<ISolution>();
            var inputAsInt = int.Parse(userInput);
            switch (inputAsInt)
            {
                case 1: solutions.AddRange([new Solutions.Day_1.SolutionA(), new Solutions.Day_1.SolutionB()]); break;
                case 2: solutions.AddRange([new Solutions.Day_2.SolutionA(), new Solutions.Day_2.SolutionB()]); break;
                case 3: solutions.AddRange([new Solutions.Day_3.SolutionA(), new Solutions.Day_3.SolutionB()]); break;
                case 4: solutions.AddRange([new Solutions.Day_4.SolutionA(), new Solutions.Day_4.SolutionB()]); break;
                case 5: solutions.AddRange([new Solutions.Day_5.SolutionA(), new Solutions.Day_5.SolutionB()]); break;
                case 6: solutions.AddRange([new Solutions.Day_6.SolutionA(), new Solutions.Day_6.SolutionB()]); break;
                case 7: solutions.AddRange([new Solutions.Day_7.SolutionA(), new Solutions.Day_7.SolutionB()]); break;
                case 8: solutions.AddRange([new Solutions.Day_8.SolutionA(), new Solutions.Day_8.SolutionB()]); break;
                case 9: solutions.AddRange([new Solutions.Day_9.SolutionA(), new Solutions.Day_9.SolutionB()]); break;
                case 10: solutions.AddRange([new Solutions.Day_10.SolutionA(), new Solutions.Day_10.SolutionB()]); break;
                case 11: solutions.AddRange([new Solutions.Day_11.SolutionA(), new Solutions.Day_11.SolutionB()]); break;
                case 12: solutions.AddRange([new Solutions.Day_12.SolutionA(), new Solutions.Day_12.SolutionB()]); break;
                case 13: solutions.AddRange([new Solutions.Day_13.SolutionA(), new Solutions.Day_13.SolutionB()]); break;
                case 14: solutions.AddRange([new Solutions.Day_14.SolutionA(), new Solutions.Day_14.SolutionB()]); break;
                case 15: solutions.AddRange([new Solutions.Day_15.SolutionA(), new Solutions.Day_15.SolutionB()]); break;
                case 16: solutions.AddRange([new Solutions.Day_16.SolutionA(), new Solutions.Day_16.SolutionB()]); break;
                case 17: solutions.AddRange([new Solutions.Day_17.SolutionA(), new Solutions.Day_17.SolutionB()]); break;
                case 18: solutions.AddRange([new Solutions.Day_18.SolutionA(), new Solutions.Day_18.SolutionB()]); break;
            }

            return solutions;
        }
    }
}
