using AdventOfCode2023.Solutions.Day_7;

namespace AdventOfCode2023Tests
{
    public class Day7Tests
    {
        List<string> testStringInput = new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("6440", result.Output);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("5905", result.Output);
        }
    }
}