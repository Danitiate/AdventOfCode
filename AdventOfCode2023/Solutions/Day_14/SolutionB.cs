using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_14
{
    /**
     *  PART 2
     *  1. Parse puzzle input into list of rows
     *  2. We will continue on the logic created in Part 1 and have a method for also tilting the platform south, west and east.
     *  3. Whenever we spin the platform, the rocks will move to a new position. We cache the response.
     *  4. Eventually, the rocks will move around in a cycle, repeating the same patterns over and over (When the result already exists in the cache)
     *  5. There is no need to cycle the platform 1 000 000 000 times, we only do it until the pattern repeats itself
     *  6. We will run the modulo operator with the size of the cycle. This will return an offset with how many more spins we need to do
     *  7. To save time, instead of running the spinning algorithm, we can simply fetch it from the cache by adding the offset value to the index of the first pattern in the cycle
     *  8. Calculate the sum of rounded rocks by counting the amount of rounded rocks per row, multiplying it by amountOfRows - currentRowIndex
     **/
    public class SolutionB : Solution
    {
        private Dictionary<string, int> PlatformToIndexCache = new Dictionary<string, int>();
        private Dictionary<int, List<string>> IndexToPlatformCache = new Dictionary<int, List<string>>();

        protected override string GetSolutionOutput()
        {
            var sumOfRoundedRocksLoad = GetSumOfRoundedRocksLoad();
            return sumOfRoundedRocksLoad.ToString();
        }

        private int GetSumOfRoundedRocksLoad()
        {
            var currentPlatform = SpinPlatformOnce(stringInputs);
            var index = 0;
            while (index <= 100000) // Set upper limit to 100000 to prevent looping forever and running out of memory
            {
                var originalIndex = GetIndexOfPlatformInCache(currentPlatform);
                if (originalIndex >= 0)
                {
                    var cycleSize = index - originalIndex;
                    var cycleOffset = (int)((1000000000L - index) % cycleSize);
                    var correctPlatform = IndexToPlatformCache[originalIndex + cycleOffset - 1];
                    return CalculatePlatformLoad(correctPlatform);
                }

                AddToCache(currentPlatform, index);
                currentPlatform = SpinPlatformOnce(currentPlatform);
                index++;
            }

            return CalculatePlatformLoad(currentPlatform);
        }

        private List<string> SpinPlatformOnce(List<string> platform)
        {
            platform = TiltPlatformNorth(platform);
            platform = TiltPlatformEastOrWest(platform, eastWards: false);
            platform = TiltPlatformSouth(platform);
            platform = TiltPlatformEastOrWest(platform, eastWards: true);
            return platform;
        }

        private List<string> TiltPlatformNorth(List<string> platform)
        {
            var northTiltedPlatform = platform.ToList();
            var firstRow = platform[0];
            for (int currentColumn = 0; currentColumn < firstRow.Length; currentColumn++)
            {
                var roundedRocksCount = 0;
                var currentRow = 0;
                for (int i = 0; i <= platform.Count; i++)
                {
                    var nextChar = i == platform.Count ? '#' : platform[i].ElementAt(currentColumn);
                    if (nextChar == 'O')
                    {
                        roundedRocksCount++;
                    }
                    else if (nextChar == '#')
                    {
                        for (int j = currentRow; j < i; j++)
                        {
                            var newString = northTiltedPlatform[j].Remove(currentColumn, 1);
                            if (roundedRocksCount > 0)
                            {
                                newString = newString.Insert(currentColumn, "O");
                                roundedRocksCount--;
                            }
                            else
                            {
                                newString = newString.Insert(currentColumn, ".");
                            }
                            northTiltedPlatform[j] = newString;
                        }

                        currentRow = i + 1;
                    }
                }
            }

            return northTiltedPlatform;
        }

        private List<string> TiltPlatformSouth(List<string> platform)
        {
            var southTiltedPlatform = platform.ToList();
            var firstRow = platform[0];
            for (int currentColumn = 0; currentColumn < firstRow.Length; currentColumn++)
            {
                var roundedRocksCount = 0;
                var currentRow = platform.Count - 1;
                for (int i = platform.Count - 1; i >= -1; i--)
                {
                    var nextChar = i == -1 ? '#' : platform[i].ElementAt(currentColumn);
                    if (nextChar == 'O')
                    {
                        roundedRocksCount++;
                    }
                    else if (nextChar == '#')
                    {
                        for (int j = currentRow; j > i; j--)
                        {
                            var newString = southTiltedPlatform[j].Remove(currentColumn, 1);
                            if (roundedRocksCount > 0)
                            {
                                newString = newString.Insert(currentColumn, "O");
                                roundedRocksCount--;
                            }
                            else
                            {
                                newString = newString.Insert(currentColumn, ".");
                            }
                            southTiltedPlatform[j] = newString;
                        }

                        currentRow = i - 1;
                    }
                }
            }

            return southTiltedPlatform;
        }

        private List<string> TiltPlatformEastOrWest(List<string> platform, bool eastWards)
        {
            var tiltedPlatform = new List<string>();
            foreach (var row in platform)
            {
                var rowParts = row.Split("#");
                var rowPartsTilted = new List<string>();
                foreach (var rowPart in rowParts)
                {
                    var amountOfRoundedRocks = rowPart.Count(c => c == 'O');
                    var emptyParts = string.Join("", Enumerable.Repeat('.', rowPart.Length - amountOfRoundedRocks));
                    var roundedRockParts = string.Join("", Enumerable.Repeat('O', amountOfRoundedRocks));
                    var newRow = eastWards ? emptyParts + roundedRockParts : roundedRockParts + emptyParts;
                    rowPartsTilted.Add(newRow);
                }
                tiltedPlatform.Add(string.Join('#', rowPartsTilted));
            }

            return tiltedPlatform;
        }

        private int GetIndexOfPlatformInCache(List<string> platform)
        {
            var cacheKey = string.Join("", platform);
            if (PlatformToIndexCache.TryGetValue(cacheKey, out var originalIndex))
            {
                return originalIndex;
            };

            return -1;
        }

        private void AddToCache(List<string> platform, int index)
        {
            var cacheKey = string.Join("", platform);
            PlatformToIndexCache.Add(cacheKey, index);
            IndexToPlatformCache.Add(index, platform);
        }

        private int CalculatePlatformLoad(List<string> tiltedPlatform)
        {
            var sum = 0;
            for (int i = 0; i < tiltedPlatform.Count; i++)
            {
                var amountOfRocks = tiltedPlatform[i].Count(c => c == 'O');
                sum += amountOfRocks * (tiltedPlatform.Count - i);
            }

            return sum;
        }
    }
}