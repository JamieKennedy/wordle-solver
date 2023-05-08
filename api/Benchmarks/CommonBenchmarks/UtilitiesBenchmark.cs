using System.Diagnostics;
using Common.Contracts;

namespace Benchmarks.CommonBenchmarks
{
    public class UtilitiesBenchmark
    {
        public static void GetPatternBenchmark(IGameData gameData, Dictionary<(string, string), string> map)
        {
            TimeSpan totalTime = TimeSpan.Zero;
            var runCount = 0;

            foreach (var wordA in gameData.PossibleWords)
            {
                foreach (var wordB in gameData.PossibleWords)
                {
                    runCount += 1;

                    totalTime += GetPattern(wordA, wordB);
                }
            }

            if (runCount > 0)
            {
                Console.WriteLine($"GetPattern Total: {totalTime}");
                Console.WriteLine($"GetPattern AVG: {(decimal) totalTime.Nanoseconds / runCount} ns");
            }
        }

        private static TimeSpan GetPattern(string wordA, string wordB)
        {
            var sw = Stopwatch.StartNew();

            Common.Utilities.GetPattern(wordA, wordB);

            var end = sw.Elapsed;

            return end;
        }

        private static TimeSpan GetPatternFromMap(string wordA, string wordB, Dictionary<(string, string), string> map)
        {
            var sw = Stopwatch.StartNew();

            Common.Utilities.GetPatternFromMap(wordA, wordB, map);

            var end = sw.Elapsed;

            return end;
        }
    }
}