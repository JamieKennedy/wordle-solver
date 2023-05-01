using Common.Contracts;

namespace Common.Models
{
    public class GameData : IGameData
    {
        public List<string> AllowedWords { get; init; }
        public List<string> PossibleWords { get; init; }
        public List<string> AllPatterns { get; init; }

        public GameData(List<string> allowedWords, List<string> possibleWords, List<string> allPatterns)
        {
            AllowedWords = allowedWords;
            PossibleWords = possibleWords;
            AllPatterns = allPatterns;
        }
    }
}