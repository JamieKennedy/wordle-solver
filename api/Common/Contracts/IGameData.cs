using Common.Models;

namespace Common.Contracts
{
    public interface IGameData
    {
        public List<string> AllowedWords { get; init; }
        public List<string> PossibleWords { get; init; }
        public List<string> AllPatterns { get; init; }
        public List<Score> Openers { get; init; }
        public Dictionary<string, string> PatternMap { get; init; }
        Dictionary<string, double> ProbabilityMap { get; init; }

    }
}