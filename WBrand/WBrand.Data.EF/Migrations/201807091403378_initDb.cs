namespace WBrand.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.AppGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AppRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AppRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.AppRoles", t => t.IdentityRole_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.AppUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.AppGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTimeOffset(nullable: false, precision: 7),
                        UserType = c.Int(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        State = c.Int(nullable: false),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(maxLength: 64),
                        UpdatedBy = c.String(maxLength: 64),
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
                "dbo.AppUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserRoles", "IdentityRole_Id", "dbo.AppRoles");
            DropForeignKey("dbo.AppUserGroups", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserGroups", "GroupId", "dbo.AppGroups");
            DropForeignKey("dbo.AppRoleGroups", "RoleId", "dbo.AppRoles");
            DropForeignKey("dbo.AppRoleGroups", "GroupId", "dbo.AppGroups");
            DropIndex("dbo.AppUserLogins", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserClaims", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserGroups", new[] { "GroupId" });
            DropIndex("dbo.AppUserGroups", new[] { "UserId" });
            DropIndex("dbo.AppUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AppUserRoles", new[] { "AppUser_Id" });
            DropIndex("dbo.AppRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.AppRoleGroups", new[] { "GroupId" });
            DropTable("dbo.AppUserLogins");
            DropTable("dbo.AppUserClaims");
            DropTable("dbo.AppUsers");
            DropTable("dbo.AppUserGroups");
            DropTable("dbo.AppUserRoles");
            DropTable("dbo.AppRoles");
            DropTable("dbo.AppRoleGroups");
            DropTable("dbo.AppGroups");
        }
    }
}
