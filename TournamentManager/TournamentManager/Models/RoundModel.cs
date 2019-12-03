using System.Collections.Generic;

namespace TournamentManager.Models
{
    public class RoundModel
    {
        public RoundModel NextRound { get; set; }
        public List<TeamModel> Teams { get; set; }
        public List<MatchModel> Matches { get; set; }
        public bool RoundFinished { get; set; }
        public bool LastRound { get; set; }

        public RoundModel(List<TeamModel> Teams)
        {
            RoundFinished = false;

            //Sets LastRound if it's last round
            if (Teams.Count == 2)
            {
                LastRound = true;
            }

            //Sets teams, adds a dummy in case of odd teams
            this.Teams = Teams;
            if (this.Teams.Count % 2 == 1)
            {
                this.Teams.Add(null);
            }

            //Fills matches
            for (int i = 0; i < Teams.Count; i += 2)
            {
                Matches.Add(new MatchModel(Teams[i], Teams[i + 1]));
            }
        }

        
        public void CreateNextRound()
        {
            List<TeamModel> AdvancingTeams = new List<TeamModel>();
            foreach (MatchModel match in Matches)
            {
                AdvancingTeams.Add(match.winner);
            }
            NextRound = new RoundModel(AdvancingTeams);
        }
        
    }
}