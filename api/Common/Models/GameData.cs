using Common.Contracts;

namespace Common.Models
{
    public class GameData : IGameData
    {
        public List<string> AllowedWords { get; init; }
        public List<string> PossibleWords { get; init; }
        public List<string> AllPatterns { get; init; }
        public List<Score> Openers { get; init; }

        public Dictionary<string, string> PatternMap { get; init; }

        public Dictionary<string, double> ProbabilityMap { get; init; }

        public GameData(List<string> allowedWords, List<string> possibleWords, List<string> allPatterns, List<Score> openers,
                        Dictionary<string, string> patternMap, Dictionary<string, double> probabilityMap)
        {
            AllowedWords = allowedWords;
            PossibleWords = possibleWords;
            AllPatterns = allPatterns;
            Openers = openers;
            PatternMap = patternMap;
            ProbabilityMap = probabilityMap;
        }
    }
}