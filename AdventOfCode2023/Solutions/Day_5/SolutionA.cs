using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_5
{
    /**
     *  PART 1
     *  1. Parse first line of input into a list of seeds
     *  2. Parse rest of inputs into several lists of SourceDestinationMap objects
     *  3. Need some helper logic:
     *      3a. Given a source number, does there exist a map that contain it? If false, return the source number itself
     *      3b. If 3a is true, given a source number, get the destination number.
     *  4. For each seed and each map, figure out its location and store it in a list
     *  5. Find the location with the lowest value and return
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_5/5.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var lowestLocationNumber = GetLowestLocationNumber(stringInputs);
            MenuPrinterService.PrintSolution(lowestLocationNumber.ToString());
        }

        private int GetLowestLocationNumber(List<string> almanac)
        {
            var lowestNumber = Int32.MaxValue;
            return lowestNumber;
        }

        internal class SourceDestinationMap()
        {
            public int DestinationRangeStart { get; set; }
            public int SourceRangeStart { get; set; }
            public int RangeLength { get; set; }
        }
    }
}