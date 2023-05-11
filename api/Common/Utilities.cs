using Common.Models;
using Newtonsoft.Json;

namespace Common
{
    public static class Utilities
    {
        public static bool IsValidPattern(string pattern)
        {
            var chars = $"{Constants.IN_WORD_CORRECT_POSITION}{Constants.NOT_IN_WORD}{Constants.IN_WORD_INCORRECT_POSITION}".ToLower();

            return pattern.Length == Constants.LENGTH && pattern.ToLower().All(c => chars.Contains(c));
        }

        public static string GetPatternFromMap(string wordA, string wordB, Dictionary<string, string> map)
        {
            return map[wordA + ':' + wordB];
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

        public static GameData GetGameData(string settingsPath, string dataPath = null)
        {
            if (dataPath == null)
            {
                dataPath = GetDataDir(settingsPath);
            }


            var allowedWords = new List<string>(File.ReadAllLines(Path.Combine(dataPath,
                @"allowed_words.txt")));
            var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(dataPath,
                @"possible_words.txt")));
            var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(dataPath,
                @"all_patterns.txt")));
            var openers = JsonConvert.DeserializeObject<List<Score>>(
                File.ReadAllText(Path.Combine(dataPath, @"openers.json")));

            var patternMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                                                                             File.ReadAllText(Path.Combine(dataPath, @"pattern_map.json")));
            var probabilityMap = JsonConvert.DeserializeObject<Dictionary<string, double>>(
                                                                                                 File.ReadAllText(Path.Combine(dataPath, @"probability_map.json")));

            return new GameData(allowedWords, possibleWords, allPatterns, openers, patternMap, probabilityMap);
        }

        public static string GetDataDir(string settingsPath)
        {
            if (!File.Exists(settingsPath)) throw new ArgumentException("Invalid Settings File");

            var options = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsPath));

            if (options == null)
            {
                throw new ArgumentException("Invalid Settings File");
            }

            return options.DataPath;
        }

    }
}