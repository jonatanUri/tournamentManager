using NUnit.Framework;
using System.Collections.Generic;
using TournamentManager.Models;

namespace TournamentManagerTests
{
    [TestFixture]
    class RoundModelTests
    {
        TeamModel team1;
        TeamModel team2;
        TeamModel team3;
        TeamModel team4;
        List<TeamModel> teams;
        RoundModel round;

        [SetUp]
        public void SetUp()
        {
            team1 = new TeamModel("Team1");
            team2 = new TeamModel("Team2");
            team3 = new TeamModel("Team3");
            team4 = new TeamModel("Team4");
            teams = new List<TeamModel>();
            teams.Add(team1);
            teams.Add(team2);
            teams.Add(team3);
            teams.Add(team4);
            round = new RoundModel(teams);
        }

        [Test]
        public void CreateNextRound_ShouldSetNextRoundWithWinningTeams()
        {
            List<TeamModel> WinnerTeams = new List<TeamModel>();
            foreach (MatchModel match in round.Matches)
            {
                match.winner = match.team1;
                WinnerTeams.Add(match.team1);
            }
            round.CreateNextRound();
            Assert.AreEqual(WinnerTeams, round.NextRound.Teams);
        }
    }
}
