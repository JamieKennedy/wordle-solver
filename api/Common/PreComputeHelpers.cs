namespace Common
{
    public class PreComputeHelpers
    {
        public static Dictionary<(string, string), string> BuildPatternMap(List<string> allWords)
        {
            Console.WriteLine("Pre-computing patterns...");
            var map = new Dictionary<(string, string), string>();

            foreach (var wordA in allWords)
            {
                foreach (var wordB in allWords)
                {
                    map[(wordA, wordB)] = Utilities.GetPattern(wordA, wordB);
                }
            }

            return map;
        }

        public static Dictionary<(string, string), double> BuildProbabilityMap(IReadOnlyCollection<string> allWords, IReadOnlyCollection<string> allPatterns,
                                                                               Dictionary<(string, string), string> patternMap)
        {
            Console.WriteLine("Pre-computing probabilities...");
            var map = new Dictionary<(string, string), double>();

            foreach (var word in allWords)
            {
                foreach (var pattern in allPatterns)
                {
                    map[(word, pattern)] = (double) allWords.Count(w => Utilities.GetPatternFromMap(word, w, patternMap).Equals(pattern)) / allWords.Count;
                }
            }

            return map;
        }
    }
}