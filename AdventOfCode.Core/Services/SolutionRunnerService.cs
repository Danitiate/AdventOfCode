using AdventOfCode.Core.Models;

namespace AdventOfCode.Core.Services
{
    public static class SolutionRunnerService
    {
        public static SolutionOutput RunSolution(Func<string> solutionMethod)
        {
            var startTime = DateTime.Now;
            var output = solutionMethod.Invoke();
            var endTime = DateTime.Now;
            var computationTime = (endTime - startTime).TotalMilliseconds;
            return new SolutionOutput(output, computationTime);
        }
    }
}
