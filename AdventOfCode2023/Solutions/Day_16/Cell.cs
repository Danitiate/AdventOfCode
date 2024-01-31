namespace AdventOfCode2023.Solutions.Day_16
{
    public class Cell
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public char Tile { get; set; }
        public bool Energized { get; set; } = false;
        public Direction PreviousDirections { get; set; } = Direction.UNKNOWN;
    }
}
