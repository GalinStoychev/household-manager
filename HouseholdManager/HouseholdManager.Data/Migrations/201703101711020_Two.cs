namespace HouseholdManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Two : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HouseholdUsers", "Household_Id", "dbo.Households");
            DropForeignKey("dbo.HouseholdUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.HouseholdUsers", new[] { "Household_Id" });
            DropIndex("dbo.HouseholdUsers", new[] { "User_Id" });
            AddColumn("dbo.Users", "CurrentHouseholdId", c => c.Guid(nullable: true));
            AddColumn("dbo.Users", "Household_Id", c => c.Guid());
            AddColumn("dbo.Households", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "CurrentHouseholdId");
            CreateIndex("dbo.Users", "Household_Id");
            CreateIndex("dbo.Households", "User_Id");
            AddForeignKey("dbo.Users", "Household_Id", "dbo.Households", "Id");
            AddForeignKey("dbo.Users", "CurrentHouseholdId", "dbo.Households", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Households", "User_Id", "dbo.Users", "Id");
            DropTable("dbo.HouseholdUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HouseholdUsers",
                c => new
                    {
                        Household_Id = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Household_Id, t.User_Id });
            
            DropForeignKey("dbo.Households", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CurrentHouseholdId", "dbo.Households");
            DropForeignKey("dbo.Users", "Household_Id", "dbo.Households");
            DropIndex("dbo.Households", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Household_Id" });
            DropIndex("dbo.Users", new[] { "CurrentHouseholdId" });
            DropColumn("dbo.Households", "User_Id");
            DropColumn("dbo.Users", "Household_Id");
            DropColumn("dbo.Users", "CurrentHouseholdId");
            CreateIndex("dbo.HouseholdUsers", "User_Id");
            CreateIndex("dbo.HouseholdUsers", "Household_Id");
            AddForeignKey("dbo.HouseholdUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HouseholdUsers", "Household_Id", "dbo.Households", "Id", cascadeDelete: true);
        }
    }
}
