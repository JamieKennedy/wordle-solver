using System.Diagnostics;
using Common.Contracts;
using Common.Models;

namespace Benchmarks.SolverBenchmarks
{
    public static class SolverUtilitiesBenchmarks
    {
        public static void CalcProbabilityBenchmarks(IGameData gameData)
        {
            TimeSpan totalTime = TimeSpan.Zero;
            var runCount = 0;
            IReadOnlyCollection<string> possibleWords = gameData.PossibleWords;
            var probabilityMap = gameData.ProbabilityMap;

            foreach (var word in possibleWords)
            {
                foreach (var pattern in gameData.AllPatterns)
                {
                    runCount += 1;

                    totalTime += CalcProbability(word, pattern, ref probabilityMap);
                }
            }

            if (runCount > 0)
            {
                Console.WriteLine($"CalcProbability Total: { totalTime}");
            }
        }

        private static TimeSpan CalcProbability(string word, string pattern, ref Dictionary<(string, string), double> probabilityMap)
        {
            var sw = Stopwatch.StartNew();
            var prob = Solver.SolverUtilities.CalcProbabilityFromMap(word, pattern, probabilityMap);
            var elapsed = sw.Elapsed;
            return elapsed;
        }
    }
}