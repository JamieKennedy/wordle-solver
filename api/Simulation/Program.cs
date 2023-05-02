// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Reflection;
using System.Text;
using Common.Contracts;
using Common.Models;
using Simulation;

var gameData = SimulationUtilities.ConfigureGameData();

var scores = new Dictionary<string, decimal>();

Console.OutputEncoding = Encoding.UTF8;
var openerCount = 1;

foreach (var openingWord in gameData.PossibleWords)
{
    var totalScore = 0;
    var gameCount = 0;

    foreach (var answer in gameData.PossibleWords)
    {
        gameCount += 1;

        var simData = SimulationUtilities.Play(openingWord, answer, gameData);
        SimulationUtilities.PrintSimulation(openingWord, answer, simData, gameCount, openerCount, gameData.PossibleWords.Count);

        totalScore += simData.RoundCount;

    }

    var avg = (decimal)totalScore / gameCount;

    scores[openingWord] = avg;
    Console.WriteLine($"{openingWord} : {avg}");

    openerCount += 1;

}

Console.WriteLine("----------------");

scores = scores.OrderByDescending(x => x.Value).ToDictionary(y => y.Key, y => y.Value);

foreach (var score in scores)
{
    Console.WriteLine($"{score.Key} : {score.Value}");
}