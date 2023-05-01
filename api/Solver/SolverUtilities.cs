﻿using System.Collections;
using System.Security.AccessControl;
using Common;

namespace Solver
{
    public static class SolverUtilities
    {
        public static List<string> GetMatchingWords(string guess, string pattern, List<string> possibleWords)
        {
            return possibleWords.Where(word => Utilities.GetPattern(guess, word).Equals(pattern)).ToList();
        }

        private static double CalcProbability(string guess, string pattern, IReadOnlyCollection<string> allWords)
        {
            var count = (double) allWords.Count(word => Utilities.GetPattern(guess, word).Equals(pattern));
            return count / allWords.Count;
        }

        private static double CalcInformation(double probability)
        {
            return probability == 0 ? 0 : Math.Log2(1 / probability);
        }

        public static double CalcExpectedInformation(string guess, IEnumerable<string> allPatterns, List<string> allWords)
        {
            return allPatterns.Aggregate(0d, (acc, pattern) =>
            {
                var probability = CalcProbability(guess, pattern, allWords);
                return acc + (probability * CalcInformation(probability));

            });
        }
    }
}