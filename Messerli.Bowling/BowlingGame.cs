using System.Collections.Generic;
using System.Linq;

namespace Messerli.Bowling
{
    public class BowlingGame
    {
        private const int FrameMaxScore = 10;
        private const int FrameCount = 10;

        private readonly IList<int> _scores = new List<int>();

        public int Score => CalculateScore();

        public void Roll(int i) => _scores.Add(i);

        private int CalculateScore()
            => Enumerable.Range(0, FrameCount)
                .Aggregate(PartialResult.Zero, (result, _) => AddRoll(result))
                .Score;

        private PartialResult AddRoll(PartialResult result)
            => new(
                result.Score + GetFrameScore(result.Roll),
                result.Roll + GetFrameRolls(result.Roll));

        private int GetFrameScore(int roll)
            => _scores.Skip(roll).Take(GetCountingRolls(roll)).Sum();

        private int GetFrameRolls(int roll)
            => IsStrike(roll) ? 1 : 2;

        private int GetCountingRolls(int roll)
            => IsStrike(roll) || IsSpare(roll) ? 3 : 2;

        private bool IsStrike(int roll)
            => _scores[roll] == FrameMaxScore;

        private bool IsSpare(int roll)
            => _scores[roll] + _scores[roll + 1] == FrameMaxScore;

        private sealed record PartialResult(int Score, int Roll)
        {
            public static PartialResult Zero => new(0, 0);
        }
    }
}
