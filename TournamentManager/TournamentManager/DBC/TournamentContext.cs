using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TournamentManager.Models;

namespace TournamentManager.DBC
{
    public class TournamentContext : DbContext
    {
        public DbSet<TournamentModel> tournaments { get; set; }
        public DbSet<MatchModel> matches { get; set; }
        public DbSet<TeamModel> teams { get; set; }
        public DbSet<RoundModel> rounds { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}