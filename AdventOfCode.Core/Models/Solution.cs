using AdventOfCode.Core.Services;

namespace AdventOfCode.Core.Models
{
    public abstract class Solution : ISolution
    {
        protected List<string> stringInputs = new List<string>();

        public SolutionOutput Solve()
        {
            var filePath = GetFilePath();
            stringInputs = FileReaderService.ReadFile(filePath);
            if (stringInputs.Count > 1) 
            {
                return SolutionRunnerService.RunSolution(GetSolutionOutput);
            }
            else
            {
                return SolutionOutput.MissingInputError(filePath);
            }
        }

        public SolutionOutput TestSolve(List<string> testStringInputs)
        {
            stringInputs = testStringInputs;
            return SolutionRunnerService.RunSolution(GetSolutionOutput);
        }

        private string GetFilePath()
        {
            var childSolutionTypeNamespace = this.GetType().Namespace;
            var day = childSolutionTypeNamespace!.Split(".").Last();
            var dayValue = day.Split("_").Last();
            return $"Solutions/{day}/{dayValue}.in";
        }

        protected abstract string GetSolutionOutput();
    }
}