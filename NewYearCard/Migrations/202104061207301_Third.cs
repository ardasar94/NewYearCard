namespace NewYearCard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cards", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
