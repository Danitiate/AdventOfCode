using AdventOfCode2023.Solutions.Day_3;

namespace AdventOfCode2023Tests
{
    public class Day3Tests
    {
        List<string> testStringInput = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("4361", result);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("467835", result);
        }
    }
}