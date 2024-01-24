namespace AdventOfCode2023.Solutions.Day_10
{
    public class Pipe()
    {
        public (int, int) Coordinates { get; set; }
        public char PipeShape { get; set; }
        public int DistanceFromStartingPoint { get; set; }
        public Pipe? PreviousPipe { get; set; }
        public Pipe? NextPipe { get; set; }
    }
}