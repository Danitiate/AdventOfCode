using AdventOfCode.Core.Services;

namespace AdventOfCode.Core.Models
{
    public abstract class Solution : ISolution
    {
        protected List<string> stringInputs = new List<string>();

        public void Solve()
        {
            var filePath = GetFilePath();
            stringInputs = FileReaderService.ReadFile(filePath);
            if (stringInputs.Count > 1) 
            {
                SolutionRunnerService.RunSolution(GetSolutionOutput);
            }
            else
            {
                MenuPrinterService.PrintMissingInput(filePath);
            }
        }

        public string TestSolve(List<string> testStringInputs)
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