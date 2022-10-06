namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "ISBN13", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ISBN13", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
