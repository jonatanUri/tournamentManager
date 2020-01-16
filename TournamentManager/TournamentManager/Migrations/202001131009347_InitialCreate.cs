namespace TournamentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        team1Score = c.Int(nullable: false),
                        team2Score = c.Int(nullable: false),
                        matchState = c.Int(nullable: false),
                        team1_Id = c.Int(),
                        team2_Id = c.Int(),
                        winner_Id = c.Int(),
                        RoundModel_Id = c.Int(),
                        TournamentModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamModel", t => t.team1_Id)
                .ForeignKey("dbo.TeamModel", t => t.team2_Id)
                .ForeignKey("dbo.TeamModel", t => t.winner_Id)
                .ForeignKey("dbo.RoundModel", t => t.RoundModel_Id)
                .ForeignKey("dbo.TournamentModel", t => t.TournamentModel_Id)
                .Index(t => t.team1_Id)
                .Index(t => t.team2_Id)
                .Index(t => t.winner_Id)
                .Index(t => t.RoundModel_Id)
                .Index(t => t.TournamentModel_Id);
            
            CreateTable(
                "dbo.TeamModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoundModel_Id = c.Int(),
                        TournamentModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoundModel", t => t.RoundModel_Id)
                .ForeignKey("dbo.TournamentModel", t => t.TournamentModel_Id)
                .Index(t => t.RoundModel_Id)
                .Index(t => t.TournamentModel_Id);
            
            CreateTable(
                "dbo.RoundModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoundFinished = c.Boolean(nullable: false),
                        LastRound = c.Boolean(nullable: false),
                        NextRound_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoundModel", t => t.NextRound_Id)
                .Index(t => t.NextRound_Id);
            
            CreateTable(
                "dbo.TournamentModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParalellMatches = c.Int(nullable: false),
                        TeamSize = c.Int(nullable: false),
                        CurrentRound_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoundModel", t => t.CurrentRound_Id)
                .Index(t => t.CurrentRound_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamModel", "TournamentModel_Id", "dbo.TournamentModel");
            DropForeignKey("dbo.TournamentModel", "CurrentRound_Id", "dbo.RoundModel");
            DropForeignKey("dbo.MatchModel", "TournamentModel_Id", "dbo.TournamentModel");
            DropForeignKey("dbo.TeamModel", "RoundModel_Id", "dbo.RoundModel");
            DropForeignKey("dbo.RoundModel", "NextRound_Id", "dbo.RoundModel");
            DropForeignKey("dbo.MatchModel", "RoundModel_Id", "dbo.RoundModel");
            DropForeignKey("dbo.MatchModel", "winner_Id", "dbo.TeamModel");
            DropForeignKey("dbo.MatchModel", "team2_Id", "dbo.TeamModel");
            DropForeignKey("dbo.MatchModel", "team1_Id", "dbo.TeamModel");
            DropIndex("dbo.TournamentModel", new[] { "CurrentRound_Id" });
            DropIndex("dbo.RoundModel", new[] { "NextRound_Id" });
            DropIndex("dbo.TeamModel", new[] { "TournamentModel_Id" });
            DropIndex("dbo.TeamModel", new[] { "RoundModel_Id" });
            DropIndex("dbo.MatchModel", new[] { "TournamentModel_Id" });
            DropIndex("dbo.MatchModel", new[] { "RoundModel_Id" });
            DropIndex("dbo.MatchModel", new[] { "winner_Id" });
            DropIndex("dbo.MatchModel", new[] { "team2_Id" });
            DropIndex("dbo.MatchModel", new[] { "team1_Id" });
            DropTable("dbo.TournamentModel");
            DropTable("dbo.RoundModel");
            DropTable("dbo.TeamModel");
            DropTable("dbo.MatchModel");
        }
    }
}
