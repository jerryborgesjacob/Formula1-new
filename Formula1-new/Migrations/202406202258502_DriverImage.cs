namespace Formula1_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "Image");
        }
    }
}
