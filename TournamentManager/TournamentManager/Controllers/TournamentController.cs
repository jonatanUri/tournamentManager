using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TournamentManager.Models;
using TournamentManager.DBC;

namespace TournamentManager.Controllers
{

    public class TournamentController : Controller
    {
        TournamentContext tournamentContext = new TournamentContext();

        // GET: Tournament
        public ActionResult Index()
        {
            var tournaments = tournamentContext.tournaments
                .Include(t => t.Teams)
                .Include(t => t.CurrentMatches)
                .Include(t => t.CurrentRound)
                .Include(t => t.CurrentRound.Matches).ToList();
            return View(tournaments);
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            TournamentModel tournament = tournamentContext.tournaments
                .Include(t => t.Teams)
                .Include(t => t.CurrentMatches)
                .Include(t => t.CurrentMatches.Select(cm => cm.team1))
                .Include(t => t.CurrentMatches.Select(cm => cm.team2))
                .Include(t => t.CurrentMatches.Select(cm => cm.winner))
                .Include(t => t.CurrentRound)
                .Include(t => t.CurrentRound.Matches)
                .Include(t => t.CurrentRound.Matches.Select(m => m.team1))
                .Include(t => t.CurrentRound.Matches.Select(m => m.team2))
                .Include(t => t.CurrentRound.Matches.Select(m => m.winner))
                .AsQueryable()
                .First(t => t.Id == id);
            return View(tournament);
        }
        
        //POST: Tournament/Details/5
        [HttpPost]
        public ActionResult Details(FormCollection collection, int id)
        {
            var matchId = Convert.ToInt32(collection["MatchId"]);
            tournamentContext.matches
                .Include(m => m.team1)
                .Include(m => m.team2)
                .AsQueryable()
                .First(m => m.Id == matchId)
                .SetResult(Convert.ToInt32(collection["Team1Score"]), Convert.ToInt32(collection["Team2Score"]));
            tournamentContext.SaveChanges();
            tournamentContext.tournaments
                .Include("Teams")
                .Include(t => t.CurrentMatches)
                .Include(t => t.CurrentMatches.Select(cm => cm.team1))
                .Include(t => t.CurrentMatches.Select(cm => cm.team2))
                .Include(t => t.CurrentMatches.Select(cm => cm.winner))
                .Include(t => t.CurrentRound)
                .Include(t => t.CurrentRound.Teams)
                .Include(t => t.CurrentRound.Matches)
                .Include(t => t.CurrentRound.Matches.Select(m => m.team1))
                .Include(t => t.CurrentRound.Matches.Select(m => m.team2))
                .Include(t => t.CurrentRound.Matches.Select(m => m.winner))
                .AsQueryable()
                .First(t => t.CurrentMatches.Any(cm => cm.Id == matchId))
                .SetCurrentMatches();
            tournamentContext.SaveChanges();
            return RedirectToAction("Details", id);
        }

        // GET: Tournament/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Tournament/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
                List<TeamModel> teams = new List<TeamModel>();
                foreach (string teamName in collection["TeamNames[]"].Split(','))
                {
                    teams.Add(new TeamModel(teamName));
                }
                var tournament = new TournamentModel(collection["Name"], teams, Convert.ToInt32(collection["ParalellMatches"]));
                tournament.SetCurrentMatches();
                tournamentContext.tournaments.Add(tournament);
                tournamentContext.SaveChanges();
                return RedirectToAction("Index");
        }

        

        // GET: Tournament/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tournament/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournament/Delete/5
        public ActionResult Delete(int id)
        {
            tournamentContext.tournaments
                .Remove(tournamentContext.tournaments
                .Include("Teams")
                .Include(t => t.CurrentMatches)
                .Include(t => t.CurrentMatches.Select(cm => cm.team1))
                .Include(t => t.CurrentMatches.Select(cm => cm.team2))
                .Include(t => t.CurrentMatches.Select(cm => cm.winner))
                .Include(t => t.CurrentRound)
                .Include(t => t.CurrentRound.Teams)
                .Include(t => t.CurrentRound.Matches)
                .Include(t => t.CurrentRound.Matches.Select(m => m.team1))
                .Include(t => t.CurrentRound.Matches.Select(m => m.team2))
                .Include(t => t.CurrentRound.Matches.Select(m => m.winner))
                .AsQueryable()
                .First(t => t.Id == id));
            tournamentContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Tournament/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
