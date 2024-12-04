using AdventOfCode2023.Solutions.Day_6;

namespace AdventOfCode2023Tests
{
    public class Day6Tests
    {
        List<string> testStringInput = new List<string>
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("288", result);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("71503", result);
        }
    }
}