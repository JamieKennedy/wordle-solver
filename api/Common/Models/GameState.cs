namespace Common.Models
{
    public class GameState
    {
        public string Guess { get; init; }
        public string Pattern { get; init; }

        public GameState(string guess, string pattern)
        {
            Guess = guess;
            Pattern = pattern;
        }
    }
}