using System.Reflection;
using Newtonsoft.Json;
using PreCompute;

var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
    @"Data/possible_words.txt")));
var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
    @"Data/all_patterns.txt")));

var basePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

if (basePath == null) return;
var dataPath = Path.Combine(basePath , "Data");

var patternMap = PreComputeHelpers.BuildPatternMap(possibleWords);

File.WriteAllText(Path.Combine(dataPath, "pattern_map.json"), JsonConvert.SerializeObject(patternMap));

var probabilityMap = PreComputeHelpers.BuildProbabilityMap(possibleWords, allPatterns, patternMap);

File.WriteAllText(Path.Combine(dataPath, "probability_map.json"), JsonConvert.SerializeObject(probabilityMap));