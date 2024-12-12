using AdventOfCode2023.Solutions.Day_13;

namespace AdventOfCode2023Tests
{
    public class Day13Tests
    {
        List<string> testStringInput = new List<string>
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };

        [Fact]
        public void SolutionA1()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("405", result.Output);
        }

        [Fact]
        public void SolutionA2()
        {
            var sut = new SolutionA();
            List<string> testStringInput = new List<string>
            {
                "........#.#",
                ".##..##.###"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("4", result.Output);
        }

        [Fact]
        public void SolutionB1()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("400", result.Output);
        }
    }
}