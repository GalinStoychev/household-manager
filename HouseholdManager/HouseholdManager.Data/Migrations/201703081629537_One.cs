namespace HouseholdManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        UserId = c.String(maxLength: 128),
                        CommentContent = c.String(),
                        CreatedOnDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CategoryId = c.Guid(nullable: false),
                        HouseholdId = c.Guid(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaid = c.Boolean(nullable: false),
                        AssignedUserId = c.String(maxLength: 128),
                        PaidById = c.String(maxLength: 128),
                        DueDate = c.DateTime(nullable: false),
                        PaidOnDate = c.DateTime(nullable: false),
                        CreateOnDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedUserId)
                .ForeignKey("dbo.ExpenseCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Households", t => t.HouseholdId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.PaidById)
                .Index(t => t.CategoryId)
                .Index(t => t.HouseholdId)
                .Index(t => t.AssignedUserId)
                .Index(t => t.PaidById);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Households",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Image = c.Binary(),
                        Address = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.HouseholdUsers",
                c => new
                    {
                        Household_Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Household_Id, t.User_Id })
                .ForeignKey("dbo.Households", t => t.Household_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Household_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ExpenseComments",
                c => new
                    {
                        Expense_Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Comment_Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                    })
                .PrimaryKey(t => new { t.Expense_Id, t.Comment_Id })
                .ForeignKey("dbo.Expenses", t => t.Expense_Id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .Index(t => t.Expense_Id)
                .Index(t => t.Comment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Expenses", "PaidById", "dbo.Users");
            DropForeignKey("dbo.Expenses", "HouseholdId", "dbo.Households");
            DropForeignKey("dbo.Expenses", "CategoryId", "dbo.ExpenseCategories");
            DropForeignKey("dbo.ExpenseComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.ExpenseComments", "Expense_Id", "dbo.Expenses");
            DropForeignKey("dbo.Expenses", "AssignedUserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.HouseholdUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.HouseholdUsers", "Household_Id", "dbo.Households");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.ExpenseComments", new[] { "Comment_Id" });
            DropIndex("dbo.ExpenseComments", new[] { "Expense_Id" });
            DropIndex("dbo.HouseholdUsers", new[] { "User_Id" });
            DropIndex("dbo.HouseholdUsers", new[] { "Household_Id" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Expenses", new[] { "PaidById" });
            DropIndex("dbo.Expenses", new[] { "AssignedUserId" });
            DropIndex("dbo.Expenses", new[] { "HouseholdId" });
            DropIndex("dbo.Expenses", new[] { "CategoryId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.ExpenseComments");
            DropTable("dbo.HouseholdUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.Households");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Expenses");
            DropTable("dbo.Comments");
        }
    }
}
