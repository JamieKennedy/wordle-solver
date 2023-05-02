using System.Collections;
using Common;
using Common.Contracts;
using Common.Models;
using static Solver.SolverUtilities;

namespace Solver
{
    public static class Solver
    {
        public static List<Response> Solve(IGameData gameData, List<GameState>? states)
        {
            List<Response> responses = new List<Response>();
            // If no state then assume it is state of the game and return a pre calculated value
            if (states == null || !states.Any()) return new List<Response>() { new Response("SLATE", 1) };

            // init to all words
            var possibleWords = gameData.PossibleWords;

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var state in states)
            {
                // Get the possible words for this game state
                var filteredWords = GetMatchingWords(state.Guess, state.Pattern, possibleWords);

                // Calculate expected information of each word and rank in descending order
                responses = filteredWords.AsParallel().Select(word => new Response(word, CalcExpectedInformation(word, gameData.AllPatterns, filteredWords)))
                    .OrderByDescending(option => option.Score).ToList();

                possibleWords = responses.Select(response => response.Word).ToList();
            }

            return responses;


        }
    }
}