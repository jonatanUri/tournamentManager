using System;

namespace TournamentManager.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public TeamModel team1 { get; set; }
        public TeamModel team2 { get; set; }
        public int team1Score { get; set; }
        public int team2Score { get; set; }
        public TeamModel winner { get; set; }
        public MatchState MatchState { get; set; }

        public MatchModel() { }

        public MatchModel(TeamModel team1, TeamModel team2)
        {
            this.team1 = team1;
            this.team2 = team2;
            MatchState = MatchState.WAITING;

            SetWinnerIfSolo();
        }

        private void SetWinnerIfSolo()
        {
            if (team1 == null)
            {
                winner = team2;
                MatchState = MatchState.FINISHED;
            }
            if (team2 == null)
            {
                winner = team1;
                MatchState = MatchState.FINISHED;
            }
        }

        public void SetResult(int team1Score, int team2Score)
        {
            if(team1Score > team2Score)
            {
                winner = team1;
            }
            else
            {
                winner = team2;
            }
            MatchState = MatchState.FINISHED;
        }

        public override string ToString()
        {
            return String.Format("Team 1 name: {0} score: {1} Team 2 name: {2} score: {3} State: {4} Winner: {5}",
                team1, team1Score, team2, team2Score, MatchState, winner);
        }
    }
}