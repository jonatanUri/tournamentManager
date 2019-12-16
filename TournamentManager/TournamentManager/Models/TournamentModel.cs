using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentManager.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamModel> Teams { get; set; }
        public int ParalellMatches { get; set; }
        public List<MatchModel> CurrentMatches { get; set; }
        public RoundModel CurrentRound { get; set; }

        public TournamentModel(string Name, List<TeamModel> Teams, int ParalellMatches)
        {
            this.Name = Name;
            this.Teams = Teams;
            this.ParalellMatches = ParalellMatches;
            CurrentRound = new RoundModel(this.Teams);
        }

        //Sets WAITING matches to CurrentMatches
        public void SetCurrentMatches()
        {
            int counter = 0;
            List<MatchModel> NextMatches = new List<MatchModel>();
            foreach (MatchModel match in CurrentRound.Matches)
            {
                if (match.matchState == MatchState.WAITING)
                {
                    match.matchState = MatchState.INPROGRESS;
                    NextMatches.Add(match);
                    counter++;
                    if (counter == ParalellMatches)
                    {
                        CurrentMatches = NextMatches;
                        return;
                    }
                }
            }
            //Creates next round if no more matches are WAITING
            if (counter == 0)
            {
                CurrentRound.RoundFinished = true;
                if (!CurrentRound.LastRound)
                {
                    CurrentRound.CreateNextRound();
                }
            }
            CurrentMatches = NextMatches;
        }
        
    }
}