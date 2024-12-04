namespace AdventOfCode.Core.Services
{
    public static class SolutionRunnerService
    {
        public static string RunSolution(Func<string> solutionMethod)
        {
            var startTime = DateTime.Now;
            var output = solutionMethod.Invoke();
            var endTime = DateTime.Now;
            MenuPrinterService.PrintSolution(output);
            var computationTime = (endTime - startTime).TotalMilliseconds;
            MenuPrinterService.PrintComputationTime(computationTime);
            return output;
        }
    }
}
