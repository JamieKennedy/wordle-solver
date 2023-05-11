using Common;
using Newtonsoft.Json;
using PreCompute;

const string settingsPath = "settings.json";

var dataPath = Utilities.GetDataDir(settingsPath);

if (string.IsNullOrEmpty(dataPath)) return;

var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(dataPath,
    "possible_words.txt")));
var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(dataPath,
    "all_patterns.txt")));

var patternMap = PreComputeHelpers.BuildPatternMap(possibleWords);

File.WriteAllText(Path.Combine(dataPath, "pattern_map.json"), JsonConvert.SerializeObject(patternMap));

var probabilityMap = PreComputeHelpers.BuildProbabilityMap(possibleWords, allPatterns, patternMap);

File.WriteAllText(Path.Combine(dataPath, "probability_map.json"), JsonConvert.SerializeObject(probabilityMap));