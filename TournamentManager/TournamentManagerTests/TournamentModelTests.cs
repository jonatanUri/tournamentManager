using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentManager.Models;

namespace TournamentManagerTests
{
    [TestFixture]
    class TournamentModelTests
    {
        TeamModel team1;
        TeamModel team2;
        TeamModel team3;
        TeamModel team4;
        List<TeamModel> teams;
        TournamentModel tournament;


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
            tournament = new TournamentModel("TestTournament", teams, 1);
        }

        [Test]
        public void SetCurrentMatches_ShouldSetCurrentMatchesToMatchTeam1vsTeam2()
        {
            tournament.SetCurrentMatches();
            List<MatchModel> ExpectedMatchup = new List<MatchModel> { new MatchModel(team1, team2) };
            ExpectedMatchup[0].MatchState = MatchState.INPROGRESS;
            Assert.AreEqual(ExpectedMatchup.ToString(), tournament.CurrentMatches.ToString());
        }

    }
}
