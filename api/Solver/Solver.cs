using System.Collections;
using Common;
using Common.Contracts;
using Common.Models;
using static Solver.SolverUtilities;

namespace Solver
{
    public static class Solver
    {
        public static Response Solve(IGameData gameData, GameState? state)
        {
            var scores = new List<Score>();
            IReadOnlyCollection<string> filteredWords;
            IReadOnlyCollection<string> patterns = gameData.AllPatterns;


            // If no state then assume it is first state of the game and return a pre calculated value
            if (state?.Rounds == null || !state.Rounds.Any())
            {
                scores = gameData.EmptyResults.OrderByDescending(score => score.Value).ToList();

                return new Response(gameData.PossibleWords, scores);
            }

            // init to all words
            IReadOnlyCollection<string> possibleWords = gameData.PossibleWords;


            if (state.PossibleWords != null && state.PossibleWords.Any())
            {
                // Use the provided list of possible words, so only need to check latest round
                var round = state.Rounds.Last();
                possibleWords = state.PossibleWords;

                filteredWords = GetMatchingWords(round.Guess, round.Pattern, ref possibleWords);

                // Calculate expected information of each word and rank in descending order
                scores = filteredWords.AsParallel().Select(word => new Score(word, CalcExpectedInformation(word, ref patterns, gameData.ProbabilityMap)))
                    .OrderByDescending(option => option.Value).ToList();

                possibleWords = scores.Select(response => response.Word).ToList();
            }



            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var round in state.Rounds)
            {
                // Get the possible words for this game state
                filteredWords = GetMatchingWords(round.Guess, round.Pattern, ref possibleWords);

                // Calculate expected information of each word and rank in descending order
                scores = filteredWords.AsParallel().Select(word => new Score(word, CalcExpectedInformation(word, ref patterns, gameData.ProbabilityMap)))
                    .OrderByDescending(option => option.Value).ToList();

                possibleWords = scores.Select(response => response.Word).ToList();
            }

            return new Response(possibleWords, scores);


        }
    }
}