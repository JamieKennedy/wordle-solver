namespace Common.Models
{
    public record Response(int RoundNum, List<Score> Scores, IReadOnlyCollection<string> PossibleWords);
}