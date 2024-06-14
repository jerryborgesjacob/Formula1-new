namespace Formula1_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        EngineSupplier = c.String(),
                        TeamPoints = c.String(),
                        DriverId = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teams");
        }
    }
}
