namespace PetZen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ActType = c.Int(nullable: false),
                        PetId = c.Int(nullable: false),
                        Default = c.Boolean(nullable: false),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Species = c.Int(nullable: false),
                        Breed = c.String(),
                        Weight = c.Double(),
                        DateOfBirth = c.DateTime(),
                        MealsPerDay = c.Int(nullable: false),
                        MedAdminsPerDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PetId);
            
            CreateTable(
                "dbo.Administrations",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        PetId = c.Int(nullable: false),
                        MedId = c.Int(nullable: false),
                        AdminDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Dosage = c.Double(nullable: false),
                        DoseMeasure = c.Int(nullable: false),
                        Defalut = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Medications", t => t.MedId, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId)
                .Index(t => t.MedId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        TimesPerDay = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RefillLink = c.String(),
                    })
                .PrimaryKey(t => t.MedId);
            
            CreateTable(
                "dbo.FeedingListitems",
                c => new
                    {
                        FeedingId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        FeedDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        PetId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        AmountFed = c.Double(nullable: false),
                        Measurement = c.Int(nullable: false),
                        Default = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.FeedingId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Species = c.Int(nullable: false),
                        PurchaseLink = c.String(),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.FeedingListitems", "PetId", "dbo.Pets");
            DropForeignKey("dbo.FeedingListitems", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.Administrations", "PetId", "dbo.Pets");
            DropForeignKey("dbo.Administrations", "MedId", "dbo.Medications");
            DropForeignKey("dbo.Activities", "PetId", "dbo.Pets");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.FeedingListitems", new[] { "FoodId" });
            DropIndex("dbo.FeedingListitems", new[] { "PetId" });
            DropIndex("dbo.Administrations", new[] { "MedId" });
            DropIndex("dbo.Administrations", new[] { "PetId" });
            DropIndex("dbo.Activities", new[] { "PetId" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Foods");
            DropTable("dbo.FeedingListitems");
            DropTable("dbo.Medications");
            DropTable("dbo.Administrations");
            DropTable("dbo.Pets");
            DropTable("dbo.Activities");
        }
    }
}
