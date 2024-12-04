using AdventOfCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var lowestLocationNumber = GetLowestLocationNumber();
            return lowestLocationNumber.ToString();
        }

        private long GetLowestLocationNumber()
        {
            var locations = new List<long>();
            var seeds = GetSeedsFromAlmanac();
            var maps = new List<List<SourceDestinationMap>>()
            {
                GetSourceToDestinationMapFromAlmanac("seed-to-soil map:"),
                GetSourceToDestinationMapFromAlmanac("soil-to-fertilizer map:"),
                GetSourceToDestinationMapFromAlmanac("fertilizer-to-water map:"),
                GetSourceToDestinationMapFromAlmanac("water-to-light map:"),
                GetSourceToDestinationMapFromAlmanac("light-to-temperature map:"),
                GetSourceToDestinationMapFromAlmanac("temperature-to-humidity map:"),
                GetSourceToDestinationMapFromAlmanac("humidity-to-location map:")
            };

            foreach (var seed in seeds)
            {
                var location = GetLocationOfSeed(seed, maps);
                locations.Add(location);
            }

            return locations.Min();
        }

        private long GetLocationOfSeed(long seed, List<List<SourceDestinationMap>> mapsList)
        {
            var previousLocation = seed;
            foreach (var maps in mapsList)
            {
                previousLocation = GetDestinationOfSource(previousLocation, maps);
            }

            return previousLocation;
        }

        private long GetDestinationOfSource(long source, List<SourceDestinationMap> sourceDestinationMaps)
        {
            var sourceMap = sourceDestinationMaps
                .FirstOrDefault(sdm =>
                    sdm.SourceRangeStart <= source &&
                    sdm.SourceRangeStart + sdm.RangeLength >= source);

            if (sourceMap == null)
            {
                return source;
            }

            return sourceMap.GetDestinationForSource(source);
        }

        private List<long> GetSeedsFromAlmanac()
        {
            var seeds = stringInputs[0];
            var numbersString = seeds.Split("seeds: ", StringSplitOptions.RemoveEmptyEntries)[0];
            var numbers = numbersString.Split(" ");
            return numbers.Select(n => long.Parse(n)).ToList();
        }

        private List<SourceDestinationMap> GetSourceToDestinationMapFromAlmanac(string map)
        {
            var sourceDestinationMaps = new List<SourceDestinationMap>();
            var startIndex = stringInputs.IndexOf(map) + 1;
            for (int i = startIndex; i < stringInputs.Count; i++)
            {
                var sourceDestinationMapString = stringInputs[i];
                var sourceDestinationMapStringArray = sourceDestinationMapString.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (sourceDestinationMapStringArray.Length == 3)
                {
                    var sourceDestinationMap = new SourceDestinationMap
                    {
                        DestinationRangeStart = long.Parse(sourceDestinationMapStringArray[0]),
                        SourceRangeStart = long.Parse(sourceDestinationMapStringArray[1]),
                        RangeLength = long.Parse(sourceDestinationMapStringArray[2])
                    };
                    sourceDestinationMaps.Add(sourceDestinationMap);
                }
                else
                {
                    break;
                }
            }

            return sourceDestinationMaps;
        }
    }
}