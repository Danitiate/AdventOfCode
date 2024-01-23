using AdventOfCode2023.Solutions.Day_10;

namespace AdventOfCode2023Tests
{
    public class Day10Tests
    {
        [Fact]
        public void SolutionA1()
        {
            var sut = new SolutionA();
            List<string> testStringInput = new List<string>
            {
                "-L|F7",
                "7S-7|",
                "L|7||",
                "-L-J|",
                "L|-JF"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("4", result);
        }

        [Fact]
        public void SolutionA2()
        {
            var sut = new SolutionA();
            List<string> testStringInput = new List<string>
            {
                "..F7.",
                ".FJ|.",
                "SJ.L7",
                "|F--J",
                "LJ..."
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("8", result);
        }

        [Fact]
        public void SolutionB1()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "...........",
                ".S-------7.",
                ".|F-----7|.",
                ".||.....||.",
                ".||.....||.",
                ".|L-7.F-J|.",
                ".|..|.|..|.",
                ".L--J.L--J.",
                "..........."
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("4", result);
        }

        [Fact]
        public void SolutionB2()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                ".F----7F7F7F7F-7....",
                ".|F--7||||||||FJ....",
                ".||.FJ||||||||L7....",
                "FJL7L7LJLJ||LJ.L-7..",
                "L--J.L7...LJS7F-7L7.",
                "....F-J..F7FJ|L7L7L7",
                "....L7.F7||L7|.L7L7|",
                ".....|FJLJ|FJ|F7|.LJ",
                "....FJL-7.||.||||...",
                "....L---J.LJ.LJLJ..."
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("8", result);
        }

        [Fact]
        public void SolutionB3()
        {
            var sut = new SolutionB();
            List<string> testStringInput = new List<string>
            {
                "FF7FSF7F7F7F7F7F---7",
                "L|LJ||||||||||||F--J",
                "FL-7LJLJ||||||LJL-77",
                "F--JF--7||LJLJ7F7FJ-",
                "L---JF-JLJ.||-FJLJJ7",
                "|F|F-JF---7F7-L7L|7|",
                "|FFJF7L7F-JF7|JL---7",
                "7-L-JL7||F7|L7F-7F7|",
                "L.L7LFJ|||||FJL7||LJ",
                "L7JLJL-JLJLJL--JLJ.L"
            };

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("10", result);
        }
    }
}