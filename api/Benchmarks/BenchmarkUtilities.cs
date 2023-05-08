using System.Reflection;
using Common;
using Common.Contracts;
using Common.Models;
using Newtonsoft.Json;

namespace Benchmarks
{
    public class BenchmarkUtilities
    {
        public static IGameData ConfigureGameData()
        {
            var allowedWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/allowed_words.txt")));
            var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/possible_words.txt")));
            var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/all_patterns.txt")));
            var emptyResponse = JsonConvert.DeserializeObject<List<Score>>(
                File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/empty_result.json")));

            var patternMap = PreComputeHelpers.BuildPatternMap(possibleWords);
            var probabilityMap = PreComputeHelpers.BuildProbabilityMap(possibleWords, allPatterns, patternMap);

            return new GameData(allowedWords, possibleWords, allPatterns, emptyResponse, patternMap, probabilityMap);
        }
    }
}