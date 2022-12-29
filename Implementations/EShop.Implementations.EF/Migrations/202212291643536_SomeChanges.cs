namespace EShop.Implementations.EF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "(0)")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETUTCDATE()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETUTCDATE()")
                                },
                            }),
                        BasketId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.BasketId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "(0)")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETUTCDATE()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "GETUTCDATE()")
                                },
                            }),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BasketProducts", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Baskets", new[] { "UserId" });
            DropIndex("dbo.BasketProducts", new[] { "ProductId" });
            DropIndex("dbo.BasketProducts", new[] { "BasketId" });
            DropColumn("dbo.Orders", "Status");
            DropTable("dbo.Baskets",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "GETUTCDATE()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "(0)" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "GETUTCDATE()" },
                        }
                    },
                });
            DropTable("dbo.BasketProducts",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "GETUTCDATE()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "(0)" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "GETUTCDATE()" },
                        }
                    },
                });
        }
    }
}
