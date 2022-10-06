namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionInBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Description");
        }
    }
}
