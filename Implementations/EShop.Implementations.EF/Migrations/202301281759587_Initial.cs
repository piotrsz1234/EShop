namespace EShop.Implementations.EF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        OrderNumber = c.String(),
                        UserId = c.Long(nullable: false),
                        PaymentType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ShippingMethodId = c.Long(nullable: false),
                        AddressId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.ShippingMethods", t => t.ShippingMethodId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ShippingMethodId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        OrderId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Count = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Long(nullable: false),
                        IsInTrash = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        IsHidden = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        VatValue = c.Int(nullable: false),
                        OldVersionProductId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.OldVersionProductId)
                .Index(t => t.CategoryId)
                .Index(t => t.OldVersionProductId);
            
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
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
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
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        IsNewsletterReceiver = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        UserDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        TwoFactorEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                        Id = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        Id = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        IsAdmin = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        Name = c.String(),
                        OwnerCategoryId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.OwnerCategoryId)
                .Index(t => t.OwnerCategoryId);
            
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        FileId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FileId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        DisplayFileName = c.String(),
                        DiscName = c.String(),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPromotions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        PromotionId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.PromotionId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        Name = c.String(),
                        ProcentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartPromotionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        EndPromotionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingMethods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "((0))")
                                },
                            }),
                        InsertDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        ModificationDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getdate()")
                                },
                            }),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShippingMethodId", "dbo.ShippingMethods");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductPromotions", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.ProductPromotions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductFiles", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductFiles", "FileId", "dbo.Files");
            DropForeignKey("dbo.Products", "OldVersionProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "OwnerCategoryId", "dbo.Categories");
            DropForeignKey("dbo.BasketProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BasketProducts", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.ProductPromotions", new[] { "ProductId" });
            DropIndex("dbo.ProductPromotions", new[] { "PromotionId" });
            DropIndex("dbo.ProductFiles", new[] { "ProductId" });
            DropIndex("dbo.ProductFiles", new[] { "FileId" });
            DropIndex("dbo.Categories", new[] { "OwnerCategoryId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Baskets", new[] { "UserId" });
            DropIndex("dbo.BasketProducts", new[] { "ProductId" });
            DropIndex("dbo.BasketProducts", new[] { "BasketId" });
            DropIndex("dbo.Products", new[] { "OldVersionProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "ShippingMethodId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.ShippingMethods",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Promotions",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "EndPromotionDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "StartPromotionDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.ProductPromotions",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Files",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.ProductFiles",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Categories",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.AspNetRoles",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsAdmin",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.AspNetUserRoles",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.AspNetUserLogins",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "EmailConfirmed",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "IsNewsletterReceiver",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "LockoutEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "PhoneNumberConfirmed",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "TwoFactorEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                });
            DropTable("dbo.Baskets",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
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
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Products",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "IsHidden",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "IsInTrash",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.OrderProducts",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Orders",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
            DropTable("dbo.Addresses",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "InsertDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                    {
                        "IsDeleted",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "((0))" },
                        }
                    },
                    {
                        "ModificationDateUtc",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getdate()" },
                        }
                    },
                });
        }
    }
}
