﻿using System.Reflection;
using Common;
using Common.Contracts;
using Common.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Newtonsoft.Json;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureGameData(this IServiceCollection services)
        {
            var allowedWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/allowed_words.txt")));
            var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/possible_words.txt")));
            var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/all_patterns.txt")));
            var emptyResponse = JsonConvert.DeserializeObject<List<Score>>(
                File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/empty_result.json")));

            var patternMap = PreComputeHelpers.BuildPatternMap(possibleWords);
            var probabilityMap = PreComputeHelpers.BuildProbabilityMap(possibleWords, allPatterns, patternMap);

            services.AddSingleton<IGameData, GameData>(data => new GameData(allowedWords, possibleWords, allPatterns, emptyResponse, patternMap, probabilityMap));
        }
    }
}