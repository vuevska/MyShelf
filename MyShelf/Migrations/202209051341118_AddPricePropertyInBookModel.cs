namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPricePropertyInBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Price");
        }
    }
}
