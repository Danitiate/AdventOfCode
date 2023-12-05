using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_5
{
    /**
     *  PART 2
     *  1. Solution should follow same logic as Part 1. Only need to adjust how we get the list of seeds
     *  2. Parse every odd number into a startIndex and every even number as a range. Add to list every value between startIndex to startIndex + range
     *  3. Parse rest of inputs into several lists of SourceDestinationMap objects
     *  4. Need some helper logic:
     *      4a. Given a source number, does there exist a map that contain it? If false, return the source number itself
     *      4b. If 3a is true, given a source number, get the destination number.
     *  5. For each seed and each map, figure out its location and store it in a list
     *  6. Find the location with the lowest value and return
     **/
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_5/5.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var lowestLocationNumber = GetLowestLocationNumber(stringInputs);
            MenuPrinterService.PrintSolution(lowestLocationNumber.ToString());
        }

        private long GetLowestLocationNumber(List<string> almanac)
        {
            var locations = new List<long>();
            var seeds = GetSeedsFromAlmanac(almanac[0]);
            var seedToSoilMap = GetSourceToDestinationMapFromAlmanac(almanac, "seed-to-soil map:");
            var soilToFertilizerMap = GetSourceToDestinationMapFromAlmanac(almanac, "soil-to-fertilizer map:");
            var fertilizerToWaterMap = GetSourceToDestinationMapFromAlmanac(almanac, "fertilizer-to-water map:");
            var waterToLightMap = GetSourceToDestinationMapFromAlmanac(almanac, "water-to-light map:");
            var lightToTemperatureMap = GetSourceToDestinationMapFromAlmanac(almanac, "light-to-temperature map:");
            var temperatureToHumidityMap = GetSourceToDestinationMapFromAlmanac(almanac, "temperature-to-humidity map:");
            var humidityToLocationMap = GetSourceToDestinationMapFromAlmanac(almanac, "humidity-to-location map:");

            foreach(var seed in seeds)
            {
                var soilDestination = GetDestinationOfSource(seed, seedToSoilMap);
                var fertilizerDestination = GetDestinationOfSource(soilDestination, soilToFertilizerMap);
                var waterDestination = GetDestinationOfSource(fertilizerDestination, fertilizerToWaterMap);
                var lightDestination = GetDestinationOfSource(waterDestination, waterToLightMap);
                var temperatureDestination = GetDestinationOfSource(lightDestination, lightToTemperatureMap);
                var humidityDestination = GetDestinationOfSource(temperatureDestination, temperatureToHumidityMap);
                var locationDestination = GetDestinationOfSource(humidityDestination, humidityToLocationMap);
                locations.Add(locationDestination);
            }

            return locations.Min();
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

        private List<long> GetSeedsFromAlmanac(string seeds)
        {
            var numbersString = seeds.Split("seeds: ", StringSplitOptions.RemoveEmptyEntries)[0];
            throw new NotImplementedException();
        }

        private List<SourceDestinationMap> GetSourceToDestinationMapFromAlmanac(List<string> almanac, string map)
        {
            var sourceDestinationMaps = new List<SourceDestinationMap>();
            var startIndex = almanac.IndexOf(map) + 1;
            for (int i = startIndex; i < almanac.Count; i++)
            {
                var sourceDestinationMapString = almanac[i];
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

        internal class SourceDestinationMap()
        {
            public long DestinationRangeStart { get; set; }
            public long SourceRangeStart { get; set; }
            public long RangeLength { get; set; }

            public long GetDestinationForSource(long source)
            {
                return source + (DestinationRangeStart - SourceRangeStart);
            }
        }
    }
}