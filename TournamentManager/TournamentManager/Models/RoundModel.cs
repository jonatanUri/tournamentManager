using System.Collections.Generic;

namespace TournamentManager.Models
{
    public class RoundModel
    {
        public int Id { get; set; }
        public RoundModel NextRound { get; set; }
        public List<TeamModel> Teams { get; set; }
        public List<MatchModel> Matches { get; set; }
        public bool RoundFinished { get; set; }
        public bool LastRound { get; set; }

        public RoundModel() { }

        public RoundModel(List<TeamModel> Teams)
        {
            RoundFinished = false;
            Matches = new List<MatchModel>();
            this.Teams = Teams;

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

        
        public RoundModel CreateNextRound()
        {
            List<TeamModel> AdvancingTeams = new List<TeamModel>();
            foreach (MatchModel match in Matches)
            {
                TeamModel clone = new TeamModel();
                clone.Name = match.winner.Name;
                AdvancingTeams.Add(clone);
            }
            NextRound = new RoundModel(AdvancingTeams);
            return NextRound;
        }
        
    }
}