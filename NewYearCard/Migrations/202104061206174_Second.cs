namespace NewYearCard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserCardsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserCardsId");
            DropColumn("dbo.Cards", "ApplicationUserId");
        }
    }
}
