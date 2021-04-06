namespace NewYearCard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCards : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Cards", "ApplicationUser_Id");
            AddForeignKey("dbo.Cards", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cards", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Cards", "ApplicationUser_Id");
        }
    }
}
