using System.Runtime.InteropServices.JavaScript;
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

        public static string GetPatternFromMap(string wordA, string wordB, Dictionary<(string, string), string> map)
        {
            return map[(wordA, wordB)];
        }

        public static string GetPattern(string wordA, string wordB)
        {
            if (string.Equals(wordA, wordB, StringComparison.CurrentCultureIgnoreCase))
            {
                return new string(Constants.IN_WORD_CORRECT_POSITION, 5);
            }

            char[] pattern = new char[] { 'X', 'X', 'X', 'X', 'X' };  // default to all incorrect

            // Green Pass
            for (int i = 0; i < wordA.Length; i++)
            {
                if(wordA.ToLower()[i] == wordB.ToLower()[i])
                {
                    pattern[i] = 'G';
                }
            }

            // Yellow Pass
            var checkedChars = new List<char>();
            for (int i = 0; i < wordA.Length; i++)
            {
                if (wordB.ToLower().Contains(wordA.ToLower()[i]) && wordA.ToLower()[i] != wordB.ToLower()[i])
                {
                    if (checkedChars.Contains(wordA.ToLower()[i]))
                    {
                        // already checked so, needs to be another in the word
                        if (wordB.ToLower().Count(c => c.Equals(wordA.ToLower()[i])) > checkedChars.Count(c => c.Equals(wordA.ToLower()[i])))
                        {
                            pattern[i] = 'Y';
                        }

                        continue;
                    }


                    pattern[i] = 'Y';
                }

                checkedChars.Add(wordA.ToLower()[i]);
            }


            return new string(pattern);

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