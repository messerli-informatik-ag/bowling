using System.Linq;
using Funcky.Extensions;
using Xunit;

namespace Messerli.Bowling.Test
{
    public class BowlingGameTest
    {
        private readonly BowlingGame _game = new();

        [Fact]
        public void AGutterGameScoresZero()
        {
            RollSameScoreNTimes(0, 20);
            Assert.Equal(0, _game.Score);
        }

        [Fact]
        public void AGameOfTwentyRollsOfOneScoresTwenty()
        {
            RollSameScoreNTimes(1, 20);
            Assert.Equal(20, _game.Score);
        }

        [Fact]
        public void SpareFirstFrameThreePinsAfterRestZeroScores16()
        {
            RollSameScoreNTimes(5, 2);
            _game.Roll(3);
            RollSameScoreNTimes(0, 17);
            Assert.Equal(16, _game.Score);
        }

        [Fact]
        public void FirstFrameStrikeFollowedByThreeAndFourPinsAndAllMissesScores24()
        {
            _game.Roll(10);
            _game.Roll(3);
            _game.Roll(4);
            RollSameScoreNTimes(0, 16);
            Assert.Equal(24, _game.Score);
        }

        [Fact]
        public void PerfectGameScores300()
        {
            RollSameScoreNTimes(10, 12);
            Assert.Equal(300, _game.Score);
        }

        private void RollSameScoreNTimes(int score, int times)
            => Enumerable.Range(0, times).ForEach(_ => _game.Roll(score));
    }
}
