namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ISBN13", c => c.String());
            DropColumn("dbo.Books", "ISBN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "ISBN", c => c.String());
            DropColumn("dbo.Books", "ISBN13");
        }
    }
}
