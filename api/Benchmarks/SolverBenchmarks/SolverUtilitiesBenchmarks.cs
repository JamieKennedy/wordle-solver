using System.Diagnostics;
using System.Text;
using Common.Contracts;

namespace Benchmarks.SolverBenchmarks
{
    public static class SolverUtilitiesBenchmarks
    {
        public static void CalcProbabilityBenchmarks(IGameData gameData)
        {
            IReadOnlyCollection<string> possibleWords = gameData.PossibleWords;

            BenchmarkInterpolation(gameData, possibleWords);
            BenchmarkFormat(gameData, possibleWords);
            BenchmarkConcat(gameData, possibleWords);
            BenchmarkStringBuilder(gameData, possibleWords);
        }

        private static void BenchmarkInterpolation(IGameData gameData, IReadOnlyCollection<string> possibleWords)
        {
            var probabilityMap = gameData.ProbabilityMap;
            var runCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var word in possibleWords)
            {
                foreach (var pattern in gameData.AllPatterns)
                {
                    runCount += 1;

                    CalcProbability_Interpolation(word, pattern, ref probabilityMap);
                }
            }

            if (runCount <= 0) return;
            Console.WriteLine($"CalcProbability_Interpolation Total: {sw.Elapsed}");
            Console.WriteLine($"CalcProbability_Interpolation Avg: {(sw.Elapsed / runCount).Nanoseconds}");
        }

        private static void BenchmarkFormat(IGameData gameData, IReadOnlyCollection<string> possibleWords)
        {
            var probabilityMap = gameData.ProbabilityMap;
            var runCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var word in possibleWords)
            {
                foreach (var pattern in gameData.AllPatterns)
                {
                    runCount += 1;

                    CalcProbability_Format(word, pattern, ref probabilityMap);
                }
            }

            if (runCount <= 0) return;
            Console.WriteLine($"CalcProbability_Format Total: {sw.Elapsed}");
            Console.WriteLine($"CalcProbability_Format Avg: {(sw.Elapsed / runCount).Nanoseconds}");
        }

        private static void BenchmarkConcat(IGameData gameData, IReadOnlyCollection<string> possibleWords)
        {
            var probabilityMap = gameData.ProbabilityMap;
            var runCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var word in possibleWords)
            {
                foreach (var pattern in gameData.AllPatterns)
                {
                    runCount += 1;

                    CalcProbability_Concat(word, pattern, ref probabilityMap);
                }
            }

            if (runCount <= 0) return;
            Console.WriteLine($"CalcProbability_Concat Total: {sw.Elapsed}");
            Console.WriteLine($"CalcProbability_Concat Avg: {(sw.Elapsed / runCount).Nanoseconds}");
        }

        private static void BenchmarkStringBuilder(IGameData gameData, IReadOnlyCollection<string> possibleWords)
        {
            var probabilityMap = gameData.ProbabilityMap;
            var runCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var word in possibleWords)
            {
                foreach (var pattern in gameData.AllPatterns)
                {
                    runCount += 1;

                    CalcProbability_StringBuilder(word, pattern, ref probabilityMap);
                }
            }

            if (runCount <= 0) return;
            Console.WriteLine($"CalcProbability_StringBuilder Total: {sw.Elapsed}");
            Console.WriteLine($"CalcProbability_StringBuilder Avg: {(sw.Elapsed / runCount).Nanoseconds}");
        }

        private static void CalcProbability_Interpolation(string word, string pattern, ref Dictionary<string, double> probabilityMap)
        {
            var key = $"{word}:{pattern}";


            Solver.SolverUtilities.CalcProbabilityFromMap(key, probabilityMap);
        }

        private static void CalcProbability_Format(string word, string pattern, ref Dictionary<string, double> probabilityMap)
        {
            var key = string.Format("{0}:{1}", word, pattern);


            Solver.SolverUtilities.CalcProbabilityFromMap(key, probabilityMap);
        }

        private static void CalcProbability_Concat(string word, string pattern, ref Dictionary<string, double> probabilityMap)
        {
            var key = word + ':' + pattern;


            Solver.SolverUtilities.CalcProbabilityFromMap(key, probabilityMap);
        }

        private static void CalcProbability_StringBuilder(string word, string pattern, ref Dictionary<string, double> probabilityMap)
        {
            var sb = new StringBuilder();
            sb.Append(word);
            sb.Append(':');
            sb.Append(pattern);


            Solver.SolverUtilities.CalcProbabilityFromMap(sb.ToString(), probabilityMap);
        }
    }
}