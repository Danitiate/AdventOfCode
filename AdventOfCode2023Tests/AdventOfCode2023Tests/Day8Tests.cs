using AdventOfCode2023.Solutions.Day_8;

namespace AdventOfCode2023Tests
{
    public class Day8Tests
    {
        [Fact]
        public void SolutionA1()
        {
            var sut = new SolutionA();
            List<string> testStringInput = new List<string>
            {
                "RL",
                "",
                "AAA = (BBB, CCC)",
                "BBB = (DDD, EEE)",
                "CCC = (ZZZ, GGG)",
                "DDD = (DDD, DDD)",
                "EEE = (EEE, EEE)",
                "GGG = (GGG, GGG)",
                "ZZZ = (ZZZ, ZZZ)"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("2", result.Output);
        }

        [Fact]
        public void SolutionA2()
        {
            var sut = new SolutionA();
            List<string> testStringInput = new List<string>
            {
                "LLR",
                "",
                "AAA = (BBB, BBB)",
                "BBB = (AAA, ZZZ)",
                "ZZZ = (ZZZ, ZZZ)"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("6", result.Output);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "LR",
                "",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("6", result.Output);
        }
    }
}