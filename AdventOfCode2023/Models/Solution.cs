using AdventOfCode2023.Services;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Models
{
    public abstract class Solution : ISolution
    {
        protected List<string> stringInputs = new List<string>();

        public void Solve()
        {
            var filePath = GetFilePath();
            stringInputs = FileReaderService.ReadFile(filePath);
            var output = GetSolutionOutput();
            MenuPrinterService.PrintSolution(output);
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