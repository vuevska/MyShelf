namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookModelAddRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "CoverUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "PublicationDate", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Publisher", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Publisher", c => c.String());
            AlterColumn("dbo.Books", "PublicationDate", c => c.String());
            AlterColumn("dbo.Books", "CoverUrl", c => c.String());
        }
    }
}
