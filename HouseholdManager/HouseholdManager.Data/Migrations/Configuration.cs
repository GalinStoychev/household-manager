using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System;
using HouseholdManager.Models;

namespace HouseholdManager.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HouseholdManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HouseholdManagerDbContext context)
        {
            if (context.ExpenseCategory.Count() == 0)
            {
                AddExpenseCategories(context);
            }
        }

        private void AddExpenseCategories(HouseholdManagerDbContext context)
        {
            var list = new List<ExpenseCategory>();
            list.Add(new ExpenseCategory() { Name = "Utilities" });
            list.Add(new ExpenseCategory() { Name = "Food" });
            list.Add(new ExpenseCategory() { Name = "Cleaning" });
            list.Add(new ExpenseCategory() { Name = "Rent" });
            list.Add(new ExpenseCategory() { Name = "Furniture" });
            list.Add(new ExpenseCategory() { Name = "Kitchen" });
            list.Add(new ExpenseCategory() { Name = "Bathroom" });
            list.Add(new ExpenseCategory() { Name = "Livingroom" });
            list.Add(new ExpenseCategory() { Name = "Other" });

            foreach (var category in list)
            {
                context.ExpenseCategory.AddOrUpdate(category);
            }

            context.SaveChanges();
        }
    }
}
