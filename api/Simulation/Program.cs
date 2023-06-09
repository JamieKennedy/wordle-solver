﻿// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using Simulation;

var gameData = Common.Utilities.GetGameData();
var scores = new Dictionary<string, decimal>();
var openerCount = 1;
var totalTime = TimeSpan.Zero;
TimeSpan? averageOpenerTime = null;


Console.WriteLine("Fetching opening words...");
var openingWords = Solver.Solver.Solve(gameData, null).Scores.Select(score => score.Word).ToList();

foreach (var openingWord in openingWords)
{
    var sw = Stopwatch.StartNew();

    var totalScore = 0;
    var gameCount = 0;


    foreach (var answer in gameData.PossibleWords)
    {

        var simData = SimulationUtilities.Play(openingWord, answer, gameData);
        totalScore += simData.RoundCount;
        gameCount += 1;

        SimulationUtilities.PrintSimulation(openingWord, answer, simData, gameCount, openerCount, openingWords.Count, gameData.PossibleWords.Count, averageOpenerTime);
    }

    var avg = (decimal)totalScore / gameData.PossibleWords.Count;
    totalTime += sw.Elapsed;
    averageOpenerTime = totalTime / openerCount;

    scores[openingWord] = avg;
    Console.WriteLine($"{openingWord} : {avg}");

    openerCount += 1;
}
Console.WriteLine("----------------");

scores = scores.OrderByDescending(x => x.Value).ToDictionary(y => y.Key, y => y.Value);

foreach (var score in scores) Console.WriteLine($"{score.Key} : {score.Value}");