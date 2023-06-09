﻿using Common;
using Common.Contracts;
using Common.Models;
using Simulation.Models;

namespace Simulation
{
    public static class SimulationUtilities
    {
        public static SimulationData Play(string openingWord, string answer, IGameData gameData)
        {
            var simData = new SimulationData();


            var currentGuess = openingWord;
            var currentPattern = "";

            while (currentPattern != "GGGGG" && simData.RoundCount < 6)
            {
                simData.RoundCount += 1;

                currentPattern = Utilities.GetPattern(currentGuess, answer);
                simData.State.Rounds.Add(new Round(currentGuess, currentPattern));

                if (currentPattern != "GGGGG")
                {
                    var response = FetchOptions(simData.State, gameData);

                    if (response.Scores.Any()) currentGuess = response.Scores[0].Word.ToUpper();

                    simData.State.PossibleWords = response.PossibleWords;
                }
            }

            return simData;
        }

        private static Response FetchOptions(GameState gameState, IGameData gameData)
        {
            return Solver.Solver.Solve(gameData, gameState);
        }

        public static void PrintSimulation(string openingWord, string answer, SimulationData simData, int answerCount, int openerCount, int totalOpeners,
                                           int totalAnswers, TimeSpan? averageOpenerTime)
        {
            Console.Clear();
            foreach (var s in simData.State.Rounds)
            {
                Console.WriteLine(s.Pattern);
            }

            Console.WriteLine($"{new string('\n', 6 - simData.RoundCount)}");

            Console.WriteLine($"Opening Word: {openingWord} ({openerCount}/{totalOpeners})");
            Console.WriteLine($"Average Opening Time: {(averageOpenerTime == null ? "~" : averageOpenerTime.ToString())}");
            Console.WriteLine($"Answer: {answer} ({answerCount}/{totalAnswers})");
            Console.WriteLine($"Score: {simData.RoundCount}");
            Console.WriteLine();
        }
    }
}