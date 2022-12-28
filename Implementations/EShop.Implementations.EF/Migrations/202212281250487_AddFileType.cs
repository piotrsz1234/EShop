namespace EShop.Implementations.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OldVersionProductId", c => c.Long());
            AddColumn("dbo.Files", "Type", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "OldVersionProductId");
            AddForeignKey("dbo.Products", "OldVersionProductId", "dbo.Products", "Id");
            DropColumn("dbo.Files", "IsMiniature");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "IsMiniature", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Products", "OldVersionProductId", "dbo.Products");
            DropIndex("dbo.Products", new[] { "OldVersionProductId" });
            DropColumn("dbo.Files", "Type");
            DropColumn("dbo.Products", "OldVersionProductId");
        }
    }
}
