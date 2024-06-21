namespace Formula1_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverRemoveImage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drivers", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drivers", "Image", c => c.Binary());
        }
    }
}
