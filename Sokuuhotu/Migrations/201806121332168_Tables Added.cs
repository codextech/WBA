namespace Sokuuhotu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BidRequests",
                c => new
                    {
                        BidRequestId = c.Int(nullable: false, identity: true),
                        BidDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        BidTimes = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidRequestId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductDescription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImagesId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImagesId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BidRequests", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BidRequests", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.BidRequests", new[] { "ProductId" });
            DropIndex("dbo.BidRequests", new[] { "UserId" });
            DropTable("dbo.ProductImages");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.BidRequests");
        }
    }
}
