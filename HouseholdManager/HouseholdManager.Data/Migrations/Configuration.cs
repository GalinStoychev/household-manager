using HouseholdManager.Data.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System;

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

            if (context.Expense.Count() == 0)
            {
                AddExpenses(context);
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void AddExpenses(HouseholdManagerDbContext context)
        {
            var userOne = context.Users.FirstOrDefault(x => x.UserName == "fa@fa.com");
            var userTwo = context.Users.FirstOrDefault(x => x.UserName == "da@da.com");
            var expOne = new Expense()
            {
                Name = "buy a table",
                ExpenseCategory = context.ExpenseCategory.First(x => x.Name == "Utilities"),
                Comments = new List<Comment>() { new Comment() { User = userOne, CreatedOnDate = DateTime.Now, CommentContent = "Buy me" } },
                Cost = 123M,
                DueDate = DateTime.Now.AddDays(3),
                CreatedOnDate = DateTime.Now,
                ExpectedCost = 100M,
                Household = new Household() { Name = "u mainatown", Address = "some address", Users = new List<User>() { userOne, userTwo } },
                IsPaid = true,
                PaidBy = userTwo,
                AssignedUser = userTwo,
                PaidOnDate = DateTime.Now.AddDays(2)
            };

            context.Expense.AddOrUpdate(expOne);
            context.SaveChanges();

            var expTwo = new Expense()
            {
                Name = "buy some toilet paper",
                ExpenseCategory = context.ExpenseCategory.First(x => x.Name == "Utilities"),
                Comments = new List<Comment>()
                {
                    new Comment() { User = userTwo, CreatedOnDate = DateTime.Now, CommentContent = "Buy me again" },
                    new Comment() { User = userOne, CreatedOnDate = DateTime.Now.AddDays(1), CommentContent = "Buy me again again" }
                },
                Cost = 123M,
                DueDate = DateTime.Now.AddDays(4),
                CreatedOnDate = DateTime.Now,
                ExpectedCost = 100M,
                Household = context.Household.First(x => x.Address == "some address"),
                IsPaid = true,
                PaidBy = userOne,
                AssignedUser = userOne,
                PaidOnDate = DateTime.Now.AddDays(1)
            };

            context.Expense.AddOrUpdate(expTwo);

            context.SaveChanges();
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
