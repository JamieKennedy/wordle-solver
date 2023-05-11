using Common;

namespace Solver
{
    public static class SolverUtilities
    {
        public static List<string> GetMatchingWords(string guess, string pattern, ref IReadOnlyCollection<string> possibleWords)
        {
            return possibleWords.AsParallel().Where(word => Utilities.GetPattern(guess, word).Equals(pattern)).ToList();
        }

        public static double CalcProbability(string guess, string pattern, IReadOnlyCollection<string> allWords,
                                             Dictionary<string, string> patternMap)
        {
            return (double) allWords.Count(word => Utilities.GetPatternFromMap(guess, word, patternMap).Equals(pattern)) / allWords.Count;
        }

        public static double CalcProbabilityFromMap(string guess, string pattern, Dictionary<string, double> probabilityMap)
        {
            return probabilityMap[guess + ':' + pattern];
        }

        private static double CalcInformation(double probability)
        {
            return probability == 0 ? 0 : Math.Log2(1 / probability);
        }

        public static double CalcExpectedInformation(string guess, ref IReadOnlyCollection<string> allPatterns, Dictionary<string, double> probabilityMap)
        {
            return allPatterns.AsParallel().Sum(pattern =>
            {
                var probability = CalcProbabilityFromMap(guess, pattern, probabilityMap);
                return probability * CalcInformation(probability);
            });
        }
    }
}