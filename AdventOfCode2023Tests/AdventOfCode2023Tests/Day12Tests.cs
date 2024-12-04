using AdventOfCode2023.Solutions.Day_12;

namespace AdventOfCode2023Tests
{
    public class Day12Tests
    {
        List<string> testStringInput = new List<string>
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("21", result);
        }

        [Fact]
        public void SolutionB1()
        {
            var sut = new SolutionB();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("525152", result);
        }

        [Fact]
        public void SolutionB2()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "???.### 1,1,3"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("1", result);
        }

        [Fact]
        public void SolutionB3()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                ".??..??...?##. 1,1,3"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("16384", result);
        }

        [Fact]
        public void SolutionB4()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "?#?#?#?#?#?#?#? 1,3,1,6"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("1", result);
        }

        [Fact]
        public void SolutionB5()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "????.#...#... 4,1,1"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("16", result);
        }

        [Fact]
        public void SolutionB6()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "????.######..#####. 1,6,5"
            };
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("2500", result);
        }
    }
}