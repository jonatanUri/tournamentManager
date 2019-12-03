namespace TournamentManager.Models
{
    public class MatchModel
    {
        public TeamModel team1 { get; set; }
        public TeamModel team2 { get; set; }
        public int team1Score { get; set; }
        public int team2Score { get; set; }
        public TeamModel winner { get; set; }
        public MatchState matchState { get; set; }

        public MatchModel(TeamModel team1, TeamModel team2)
        {
            this.team1 = team1;
            this.team2 = team2;
            matchState = MatchState.WAITING;

            if (team2 == null)
            {
                winner = team1;
                matchState = MatchState.FINISHED;
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
            matchState = MatchState.FINISHED;
        }

    }
}