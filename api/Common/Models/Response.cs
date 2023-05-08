namespace Common.Models
{
    public record Response(IReadOnlyCollection<string> PossibleWords, List<Score> Scores);
}