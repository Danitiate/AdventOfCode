namespace AdventOfCode2023.Solutions.Day_5
{
    public class Seed()
    {
        public long StartSeed { get; set; }
        public long Length { get; set; }
        public long EndSeed
        {
            get
            {
                return StartSeed + Length - 1;
            }
        }
    }
}