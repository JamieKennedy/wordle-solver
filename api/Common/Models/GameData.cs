using Common.Contracts;

namespace Common.Models
{
    public class GameData : IGameData
    {
        public List<string> AllowedWords { get; init; }
        public List<string> PossibleWords { get; init; }
        public List<string> AllPatterns { get; init; }
        public List<Score> EmptyResults { get; init; }

        public Dictionary<(string, string), string> PatternMap { get; init; }

        public Dictionary<(string, string), double> ProbabilityMap { get; init; }

        public GameData(List<string> allowedWords, List<string> possibleWords, List<string> allPatterns, List<Score> emptyResults,
                        Dictionary<(string, string), string> patternMap, Dictionary<(string, string), double> probabilityMap)
        {
            AllowedWords = allowedWords;
            PossibleWords = possibleWords;
            AllPatterns = allPatterns;
            EmptyResults = emptyResults;
            PatternMap = patternMap;
            ProbabilityMap = probabilityMap;
        }
    }
}