namespace Common.Models
{
    public class GameState
    {

        public List<Round> Rounds { get; set; }
        public IReadOnlyCollection<string>? PossibleWords { get; set; }

        public GameState()
        {
            Rounds = new List<Round>();
            PossibleWords = new List<string>();
        }
    }
}