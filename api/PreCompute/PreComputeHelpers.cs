using Common;

namespace PreCompute
{
    public static class PreComputeHelpers
    {
        public static Dictionary<string, string> BuildPatternMap(List<string> allWords)
        {
            var patternCount = 0;
            var map = new Dictionary<string, string>();

            foreach (var wordA in allWords)
            {


                foreach (var wordB in allWords)
                {
                    patternCount += 1;

                    map[wordA + ':' + wordB] = Utilities.GetPattern(wordA, wordB);

                    if (patternCount % 1000 == 0 || patternCount.Equals((int)Math.Pow(allWords.Count, 2)))
                    {
                        PrintStatus(patternCount, (int) Math.Pow(allWords.Count, 2), 0, 0);
                    }
                }
            }

            return map;
        }

        public static Dictionary<string, double> BuildProbabilityMap(IReadOnlyCollection<string> allWords, IReadOnlyCollection<string> allPatterns,
                                                                       Dictionary<string, string> patternMap)
        {
            var map = new Dictionary<string, double>();
            var probabilityCount = 0;

            foreach (var word in allWords)
            {


                foreach (var pattern in allPatterns)
                {
                    probabilityCount += 1;

                    map[word + ':' + pattern] = (double) allWords.Count(w => Utilities.GetPatternFromMap(word, w, patternMap).Equals(pattern)) / allWords.Count;

                    if (probabilityCount % 100 == 0 || probabilityCount.Equals(allWords.Count * allPatterns.Count))
                    {
                        PrintStatus((int) Math.Pow(allWords.Count, 2), (int) Math.Pow(allWords.Count, 2), probabilityCount, allWords.Count * allPatterns.Count);
                    }
                }
            }

            return map;
        }

        public static void PrintStatus(int patternCount, int totalPatterns, int probabilityCount, int totalProbability)
        {
            Console.Clear();
            Console.WriteLine("Computing Patterns...");
            Console.WriteLine($"({patternCount} / {totalPatterns})");


            if (probabilityCount <= 0) return;
            Console.WriteLine();
            Console.WriteLine("Computing probabilities...");
            Console.WriteLine($"({probabilityCount} / {totalProbability})");

        }
    }
}