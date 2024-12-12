
namespace AdventOfCode.Core.Models
{
    public class SolutionOutput
    {
        public string Output { get; private set; }
        public double ComputationTimeInMs { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsMissingInput { get; private set; }

        public SolutionOutput(string output, double computationTimeInMs) 
        { 
            this.Output = output;
            this.ComputationTimeInMs = computationTimeInMs;
        }

        public static SolutionOutput MissingInputError(string filepath)
        {
            var solutionOutput = new SolutionOutput("", 0.0);
            solutionOutput.ErrorMessage = filepath;
            solutionOutput.IsMissingInput = true;
            return solutionOutput;
        }
    }
}
