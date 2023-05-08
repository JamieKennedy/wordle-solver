namespace Common
{
    public class PreComputeHelpers
    {
        public static Dictionary<(string, string), string> BuildPatternMap(List<string> allWords)
        {
            var count = 0;
            var map = new Dictionary<(string, string), string>();

            foreach (var wordA in allWords)
            {
                Console.Clear();
                Console.WriteLine("Pre-computing patterns...");
                Console.WriteLine($"({count} / {Math.Pow(allWords.Count, 2)})");

                foreach (var wordB in allWords)
                {
                    count += 1;

                    map[(wordA, wordB)] = Utilities.GetPattern(wordA, wordB);


                }
            }

            return map;
        }

        public static Dictionary<(string, string), double> BuildProbabilityMap(IReadOnlyCollection<string> allWords, IReadOnlyCollection<string> allPatterns,
                                                                               Dictionary<(string, string), string> patternMap)
        {
            var map = new Dictionary<(string, string), double>();
            var count = 0;

            foreach (var word in allWords)
            {
                Console.Clear();
                Console.WriteLine("Pre-computing patterns...");
                Console.WriteLine($"({count} / {allWords.Count * allPatterns.Count})");

                foreach (var pattern in allPatterns)
                {
                    count += 1;



                    map[(word, pattern)] = (double) allWords.Count(w => Utilities.GetPatternFromMap(word, w, patternMap).Equals(pattern)) / allWords.Count;
                }
            }

            return map;
        }
    }
}