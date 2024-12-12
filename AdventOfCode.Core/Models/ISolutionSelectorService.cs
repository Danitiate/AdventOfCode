namespace AdventOfCode.Core.Models
{
    public interface ISolutionSelectorService
    {
        public List<ISolution> GetRequestedSolutions(int selectedDay);
    }
}