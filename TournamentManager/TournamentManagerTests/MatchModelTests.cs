using NUnit.Framework;
using TournamentManager.Models;

namespace TournamentManagerTests
{
    [TestFixture]
    public class MatchModelTests
    {
        TeamModel team1;
        TeamModel team2;
        MatchModel match;

        [SetUp]
        public void SetUp()
        {
            team1 = new TeamModel("Team1");
            team2 = new TeamModel("Team2");
            match = new MatchModel(team1, team2);
        }

        [Test]
        public void SetResult_Given16And9_ShouldSetWinnerToTeam1()
        {
            match.SetResult(16, 9);
            Assert.AreEqual(team1, match.winner);
        }

        [Test]
        public void SetResult_Given1And2_ShouldSetWinnerToTeam2()
        {
            match.SetResult(1, 2);
            Assert.AreEqual(team2, match.winner);
        }

    }
}
