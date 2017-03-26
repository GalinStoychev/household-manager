using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.Controllers;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        private readonly IHouseholdService householdService;
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService, IMapingService mappingService, IHouseholdService householdService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            if (householdService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "householdService"));
            }

            this.expenseService = expenseService;
            this.householdService = householdService;
        }

        [HttpGet]
        public ActionResult Index(Guid id)
        {
            var expense = this.expenseService.GetExpense(id);
            if (expense.HouseholdId != this.webHelper.GetHouseholdIdFromCookie())
            {
                return Redirect("/Error/Unauthorized");
            }

            var mapped = this.mappingService.Map<ExpenseViewModel>(expense);

            return View(mapped);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var modelCategories = this.PrepareModelCategories();
            var model = new ExpenseViewModel() { CategoriesList = modelCategories };
            model.Users = this.PrepareModelUsers();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Cost, PaidOnDate, CreatedOn, IsPaid, PaidBy")] ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Error/BadRequest");
            }

            var householdid = this.webHelper.GetHouseholdIdFromCookie();
            this.expenseService.CreateExpense(this.webHelper.GetUserId(), model.Name, Guid.Parse(model.Category), householdid, model.ExpectedCost, model.DueDate, model.Comment, model.AssignedUser);

            return RedirectToAction("Index", "Expenses", new { name = this.webHelper.GetHouseholdNameFromCookie() });
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var expense = this.expenseService.GetExpense(Guid.Parse(id));
            if (expense.HouseholdId != this.webHelper.GetHouseholdIdFromCookie())
            {
                return Redirect("/Error/Unauthorized");
            }

            var model = this.mappingService.Map<ExpenseViewModel>(expense);
            model.CategoriesList = this.PrepareModelCategories();
            model.Users = this.PrepareModelUsers();
            model.AssignedUser = expense.AssignedUserId;
            model.Category = expense.CategoryId.ToString();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Category, AssignedUser, DueDate, ExpectedCost")] ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Error/BadRequest");
            }

            this.expenseService.UpdateExpense(model.Id, model.Name, Guid.Parse(model.Category), model.ExpectedCost, model.DueDate, model.AssignedUser);
            return RedirectToAction("Index", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id")] ExpenseViewModel model)
        {
            this.expenseService.DeleteExpense(model.Id, true);
            return RedirectToAction("Index", new { id = model.Id });
        }

        private IList<SelectListItem> PrepareModelCategories()
        {
            var expenseCateogries = this.expenseService.GetExpenseCategories();
            var modelCategories = new List<SelectListItem>();
            foreach (var category in expenseCateogries)
            {
                modelCategories.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Name });
            }

            return modelCategories;
        }

        private IList<SelectListItem> PrepareModelUsers()
        {
            var householdid = this.webHelper.GetHouseholdIdFromCookie();
            var users = this.householdService.GetHouseholdUsers(householdid);
            var modelUsers = new List<SelectListItem>();
            foreach (var user in users)
            {
                modelUsers.Add(new SelectListItem() { Value = user.Id, Text = user.FirstName + " " + user.LastName });
            }

            return modelUsers;
        }
    }
}