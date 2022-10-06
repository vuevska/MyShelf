namespace MyShelf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartsAndBookViewa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookItemViewModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ShoppingCartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ShoppingCartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookItemViewModels", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.BookItemViewModels", "BookId", "dbo.Books");
            DropIndex("dbo.BookItemViewModels", new[] { "ShoppingCartId" });
            DropIndex("dbo.BookItemViewModels", new[] { "BookId" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.BookItemViewModels");
        }
    }
}
