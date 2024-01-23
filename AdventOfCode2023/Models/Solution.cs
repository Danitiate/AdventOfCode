using AdventOfCode2023.Services;
using System.Collections.Generic;
using System.IO;
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
            if (stringInputs.Count > 1) 
            { 
                var output = GetSolutionOutput();
                MenuPrinterService.PrintSolution(output);
            }
            else
            {
                MenuPrinterService.PrintMissingInput(filePath);
            }
        }

        public string TestSolve(List<string> testStringInputs)
        {
            stringInputs = testStringInputs;
            return GetSolutionOutput();
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