using System;

namespace AdventOfCode2023.Solutions.Day_17
{
    [Flags]
    public enum Direction
    {
        UNKNOWN = 0,
        NORTH = 1,
        EAST = 2,
        SOUTH = 4,
        WEST = 8
    }
}
