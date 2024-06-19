namespace Formula1_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "DriverId");
            AddForeignKey("dbo.Teams", "DriverId", "dbo.Drivers", "DriverId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Teams", new[] { "DriverId" });
            AlterColumn("dbo.Teams", "DriverId", c => c.String());
        }
    }
}
