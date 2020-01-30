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
            tournament.CurrentMatches = new List<MatchModel>();
        }

        [Test]
        public void SetCurrentMatches_ShouldSetCurrentMatchesToMatchTeam1vsTeam2()
        {
            tournament.SetCurrentMatches();
            List<MatchModel> ExpectedMatchup = new List<MatchModel> { new MatchModel(team1, team2) };
            ExpectedMatchup[0].MatchState = MatchState.INPROGRESS;
            Assert.AreEqual(ExpectedMatchup.ToString(), tournament.CurrentMatches.ToString());
        }

        [Test]
        public void FindContainingMatch_ShouldReturnMatchThatContainsTeam3()
        {
            MatchModel match = tournament.FindContaningMatch(team3);
            Assert.IsTrue(match.team1.Equals(team3) || match.team2.Equals(team3));
        }

        [Test]
        public void FindContainingMatch_ShouldThrowExceptionForTeam5()
        {
            TeamModel team5 = new TeamModel("Team5");
            Assert.Throws<Exception>(() => tournament.FindContaningMatch(team5));
        }

        [Test]
        public void IsTeamAdvancing_ShouldReturnTrue_ForTeam1()
        {
            MatchModel testMatch = new MatchModel(team1, team2);
            testMatch.winner = team1;
            testMatch.MatchState = MatchState.FINISHED;
            tournament.CurrentMatches.Add(testMatch);
            Assert.IsTrue(tournament.IsTeamAdvancing(team1));
        }

        [Test]
        public void IsTeamAdvancing_ShouldReturnFalse_ForTeam2()
        {
            MatchModel testMatch = tournament.FindContaningMatch(team2);
            testMatch.winner = team1;
            testMatch.MatchState = MatchState.FINISHED;
            tournament.CurrentMatches.Add(testMatch);
            Assert.IsFalse(tournament.IsTeamAdvancing(team2));
        }
    }
}
