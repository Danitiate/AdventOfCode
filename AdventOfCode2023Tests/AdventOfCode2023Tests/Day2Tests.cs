using AdventOfCode2023.Solutions.Day_2;

namespace AdventOfCode2023Tests
{
    public class Day2Tests
    {
        List<string> testStringInput = new List<string>
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("8", result.Output);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("2286", result.Output);
        }
    }
}