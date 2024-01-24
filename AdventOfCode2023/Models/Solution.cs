using AdventOfCode2023.Services;
using System;
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
            var startTime = DateTime.Now;
            if (stringInputs.Count > 1) 
            { 
                var output = GetSolutionOutput();
                var endTime = DateTime.Now;
                MenuPrinterService.PrintSolution(output);
                Console.WriteLine("Computation time (ms): " + (endTime - startTime).TotalMilliseconds);
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