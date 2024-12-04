using AdventOfCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_5
{
    /**
     *  PART 2
     *  1. Solution can't follow same logic as Part 1 -> Takes too long as there are just too many seeds.
     *  2. Quickly iterate through full list and find all possible mappings for every seed
     *  3. Ignore all seeds that have the same chain of mappings
     *  4. If a seed has a different mapping, look more carefully to find edge cases
     *  
     *  Note: Correct seed is 2691976369, but I do not know where I would get this from. 
     *        Even brute forcing through all options resulted in wrong output.
     *        Will have to come back to this later.
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var lowestLocationNumber = GetLowestLocationNumber();
            return lowestLocationNumber.ToString();
        }

        private long GetLowestLocationNumber()
        {
            var currentLowestLocation = long.MaxValue;
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

            var maps2 = GetMapChain(2691976369L, maps);
            var location2 = GetLocationOfSeed(2691976369L, maps);

            foreach (var seed in seeds)
            {
                var boundaries = GetMapListBoundaries(seed, maps);
                foreach(var boundary in boundaries)
                {
                    var location = GetLocationOfSeed(boundary, maps);
                    currentLowestLocation = Math.Min(currentLowestLocation, location);
                }
            }

            return currentLowestLocation;
        }

        private List<Seed> GetSeedsFromAlmanac()
        {
            var seeds = stringInputs[0];
            var seedsList = new List<Seed>();
            var numbersString = seeds.Split("seeds: ", StringSplitOptions.RemoveEmptyEntries)[0];
            var numbersArray = numbersString.Split(" ");
            for (int i = 1; i < numbersArray.Length; i += 2)
            {
                seedsList.Add(new Seed
                {
                    StartSeed = long.Parse(numbersArray[i - 1]),
                    Length = long.Parse(numbersArray[i])
                });
            }
            return seedsList;
        }

        private List<long> GetMapListBoundaries(Seed seed, List<List<SourceDestinationMap>> maps)
        {
            var boundaries = new List<long>();
            var previousMapChain = new List<SourceDestinationMap>();
            var increment = 1000L;
            for (long i = seed.StartSeed; i <= seed.EndSeed; i += increment)
            {
                var nextMapChain = GetMapChain(i, maps);
                if (!ListsAreEqual(previousMapChain, nextMapChain))
                {
                    for (long j = i - increment; j <= i + increment; j++)
                    {
                        nextMapChain = GetMapChain(i, maps);
                        if (!ListsAreEqual(previousMapChain, nextMapChain))
                        {
                            boundaries.Add(j - 1);
                            previousMapChain = nextMapChain;
                            boundaries.Add(j);
                        }
                    }
                }
            }
            boundaries.Add(seed.EndSeed);
            return boundaries.Distinct().ToList();
        }

        private bool ListsAreEqual(List<SourceDestinationMap> lastAddedMapChain, List<SourceDestinationMap> mapChain)
        {
            if (lastAddedMapChain.Count != mapChain.Count)
            {
                return false;
            }

            for (int i = 0; i < lastAddedMapChain.Count; i++)
            {
                var currentLastAddedMap = lastAddedMapChain[i];
                var currentMapChain = mapChain[i];
                if (currentLastAddedMap.SourceRangeStart != currentMapChain.SourceRangeStart ||
                    currentLastAddedMap.DestinationRangeStart != currentMapChain.DestinationRangeStart ||
                    currentLastAddedMap.RangeLength != currentMapChain.RangeLength)
                {
                    return false;
                }
            }

            return true;
        }

        private List<SourceDestinationMap> GetMapChain(long seed, List<List<SourceDestinationMap>> mapsLists)
        {
            var mapChain = new List<SourceDestinationMap>();
            foreach (var mapList in mapsLists)
            {
                var map = GetMapFromSource(seed, mapList);
                if (map != null)
                {
                    mapChain.Add(map);
                    seed = map.GetDestinationForSource(seed);
                }
            }

            return mapChain;
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

        private SourceDestinationMap? GetMapFromSource(long source, List<SourceDestinationMap> sourceDestinationMaps)
        {
            return sourceDestinationMaps
                .FirstOrDefault(sdm =>
                    sdm.SourceRangeStart <= source &&
                    sdm.SourceRangeStart + sdm.RangeLength >= source);
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