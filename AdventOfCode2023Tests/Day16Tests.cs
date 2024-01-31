using AdventOfCode2023.Solutions.Day_16;

namespace AdventOfCode2023Tests
{
    public class Day16Tests
    {
        List<string> testStringInput = new List<string>
        {
            ".|...\\....",
            "|.-.\\.....",
            ".....|-...",
            "........|.",
            "..........",
            ".........\\",
            "..../.\\\\..",
            ".-.-/..|..",
            ".|....-|.\\",
            "..//.|...."
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("46", result);
        }
    }
}