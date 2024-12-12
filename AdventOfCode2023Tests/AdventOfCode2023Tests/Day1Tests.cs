using AdventOfCode2023.Solutions.Day_1;

namespace AdventOfCode2023Tests
{
    public class Day1Tests
    {
        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();
            var testStringInput = new List<string>
            {
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("SolutionUsingRegex: 142\nSolutionUsingLoops: 142", result.Output);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();
            var testStringInput = new List<string>
            {
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("SolutionUsingReplace: 281\nSolutionUsingRegex: 281\nSolutionUsingLoops: 281", result.Output);
        }
    }
}