using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TournamentManager.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Key]
        public List<TeamModel> Teams { get; set; }
        public int ParalellMatches { get; set; }
        public int TeamSize { get; set; }
        [Key]
        public List<MatchModel> CurrentMatches { get; set; }
        [Key]
        public RoundModel CurrentRound { get; set; }

        public TournamentModel(string Name, List<TeamModel> Teams, int ParalellMatches)
        {
            this.Name = Name;
            this.Teams = Teams.OrderBy(a => Guid.NewGuid()).ToList();
            this.ParalellMatches = ParalellMatches;
            CurrentRound = new RoundModel(this.Teams);
        }

        public TournamentModel() { }

        //Sets WAITING matches to CurrentMatches
        public void SetCurrentMatches()
        {
            int counter = 0;
            List<MatchModel> NextMatches = new List<MatchModel>();
            foreach (MatchModel match in CurrentRound.Matches)
            {
                if (match.MatchState == MatchState.INPROGRESS)
                {
                    NextMatches.Add(match);
                    counter++;
                }
                else if (match.MatchState == MatchState.WAITING)
                {
                    match.MatchState = MatchState.INPROGRESS;
                    NextMatches.Add(match);
                    counter++;
                }
                if (counter == ParalellMatches)
                {
                    CurrentMatches = NextMatches;
                    return;
                }
            }
            //Creates next round if no more matches are WAITING
            if (counter == 0)
            {
                CurrentRound.RoundFinished = true;
                if (!CurrentRound.LastRound)
                {
                    CurrentRound = CurrentRound.CreateNextRound();
                    SetCurrentMatches();
                }
                else
                {
                    CurrentMatches = new List<MatchModel>();
                }
                return;
            }
            CurrentMatches = NextMatches;
        }
        
        public bool IsTeamAdvancing(TeamModel team)
        {
            if (!CurrentRound.Teams.Contains(team))
            {
                return false;
            }
            else
            {
                MatchModel match = FindContaningMatch(team);
                if (match.MatchState == MatchState.FINISHED)
                {
                    if (!match.winner.Equals(team))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public MatchModel FindContaningMatch(TeamModel team)
        {
            foreach (MatchModel match in CurrentRound.Matches)
            {
                if (match.team1.Equals(team) || match.team2.Equals(team))
                {
                    return match;
                }
            }
            throw new Exception(String.Format("Couldn't find a match in current round that contains {0} team", team.Name));
        }
    }
}