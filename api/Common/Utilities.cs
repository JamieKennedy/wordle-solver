using System.Text;
using Microsoft.Extensions.Primitives;

namespace Common
{
    public static class Utilities
    {
        public static bool IsValidPattern(string pattern)
        {
            var chars = $"{Constants.IN_WORD_CORRECT_POSITION}{Constants.NOT_IN_WORD}{Constants.IN_WORD_INCORRECT_POSITION}".ToLower();

            return pattern.Length == Constants.LENGTH && pattern.ToLower().All(c => chars.Contains(c));
        }

        public static string GetPattern(string wordA, string wordB)
        {
            if (string.Equals(wordA, wordB, StringComparison.CurrentCultureIgnoreCase))
            {
                return new string(Constants.IN_WORD_CORRECT_POSITION, 5);
            }

            var pattern = new StringBuilder();

            for (int i = 0; i < wordA.Length; i++)
            {
                if (wordB.Contains(wordA.ToLower()[i]))
                {
                    // Letter in wordB
                    pattern.Append(wordA.ToLower()[i] == wordB[i] ? Constants.IN_WORD_CORRECT_POSITION : Constants.IN_WORD_INCORRECT_POSITION);
                    continue;
                }

                // Letter not in wordB
                pattern.Append(Constants.NOT_IN_WORD);
            }

            return pattern.ToString();
        }

        public static IEnumerable<IEnumerable<T>> GetPatterns<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPatterns(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}