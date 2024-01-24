namespace AdventOfCode2023.Solutions.Day_5
{
    public class SourceDestinationMap()
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