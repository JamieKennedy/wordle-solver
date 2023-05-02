using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Common;
using Common.Contracts;
using Common.Models;
using Newtonsoft.Json;
using Simulation.Models;

namespace Simulation
{
    public static class SimulationUtilities
    {
        public static IGameData ConfigureGameData()
        {
            var allowedWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/allowed_words.txt")));
            var possibleWords = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/possible_words.txt")));
            var allPatterns = new List<string>(File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/all_patterns.txt")));

            return new GameData(allowedWords, possibleWords, allPatterns);
        }

        public static SimulationData Play(string openingWord, string answer, IGameData gameData)
        {
            var simData = new SimulationData();

            var currentGuess = openingWord;
            var currentPattern = "";

            while(currentPattern != "GGGGG")
            {
                simData.RoundCount += 1;

                currentPattern = Utilities.GetPattern(currentGuess, answer);
                simData.State.Add(new GameState(currentGuess, currentPattern));

                if (currentPattern != "GGGGG")
                {
                    var newOptions = FetchOptions(simData.State, gameData);

                    if (newOptions.Count > 0)
                    {
                        currentGuess = newOptions[0].Word.ToUpper();
                    }
                }
            }

            return simData;
        }

        private static List<Response> FetchOptions(List<GameState> gameState, IGameData gameData)
        {
            return Solver.Solver.Solve(gameData, gameState);
        }

        public static void PrintSimulation(string openingWord, string answer, SimulationData simData, int answerCount, int openerCount, int totalGames)
        {
            Console.Clear();

            foreach (var s in simData.State)
            {
                Console.WriteLine(s.Pattern);
            }

            Console.WriteLine($"{new string('\n', 6 - simData.RoundCount)}");
            Console.WriteLine($"Opening Word: {openingWord} ({openerCount}/{totalGames})");
            Console.WriteLine($"Answer: {answer} ({answerCount}/{totalGames})");
            Console.WriteLine($"Score: {simData.RoundCount}");
            Console.WriteLine();
        }
    }
}