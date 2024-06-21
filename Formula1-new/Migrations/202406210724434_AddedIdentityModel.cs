namespace Formula1_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdentityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Racetracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        TrackName = c.String(),
                        TrackLength = c.Int(nullable: false),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.TrackId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Racetracks");
        }
    }
}
