using AdventOfCode2023.Solutions.Day_15;

namespace AdventOfCode2023Tests
{
    public class Day15Tests
    {
        List<string> testStringInput = new List<string>
        {
            "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
        };

        [Fact]
        public void SolutionA()
        {
            var sut = new SolutionA();

            var result = sut.TestSolve(testStringInput);

            Assert.Equal("1320", result);
        }

        [Fact]
        public void SolutionB()
        {
            var sut = new SolutionB();
            var result = sut.TestSolve(testStringInput);

            Assert.Equal("145", result);
        }
    }
}