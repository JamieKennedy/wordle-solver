namespace Common.Models
{
    public class Round
    {
        public string Guess { get; init; }
        public string Pattern { get; init; }

        public Round(string guess, string pattern)
        {
            Guess = guess;
            Pattern = pattern;
        }
    }
}