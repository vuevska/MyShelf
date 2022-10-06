namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReuquiredPropertyToISBN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ISBN13", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ISBN13", c => c.String());
        }
    }
}
