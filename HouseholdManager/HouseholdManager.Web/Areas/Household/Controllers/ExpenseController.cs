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
        public ActionResult Index(string id)
        {
            var expense = this.expenseService.GetExpense(Guid.Parse(id));
            var mapped = this.mappingService.Map<ExpenseViewModel>(expense);

            return View(mapped);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var expenseCateogries = this.expenseService.GetExpenseCategories();
            var modelCategories = new List<SelectListItem>();
            foreach (var category in expenseCateogries)
            {
                modelCategories.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Name });
            }

            var model = new ExpenseViewModel() { Categories = modelCategories };

            var householdid = this.webHelper.GetHouseholdIdFromCookie();
            var users = this.householdService.GetHouseholdUsers(householdid);
            model.Users = new List<SelectListItem>();
            model.Users.Add(new SelectListItem());
            foreach (var user in users)
            {
                model.Users.Add(new SelectListItem() { Value = user.Id, Text = user.FirstName + " " + user.LastName });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Cost, PaidOnDate, CreatedOn, IsPaid, PaidBy")] ExpenseViewModel model)
        {
            var householdid = this.webHelper.GetHouseholdIdFromCookie();
            this.expenseService.CreateExpense(this.webHelper.GetUserId(), model.Name, Guid.Parse(model.Category), householdid, model.ExpectedCost, model.DueDate, model.Comment, model.AssignedUser);

            return RedirectToAction("Index", "Expenses", new { name = this.webHelper.GetHouseholdNameFromCookie() });
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var expense = this.expenseService.GetExpense(Guid.Parse(id));
            var mapped = this.mappingService.Map<ExpenseViewModel>(expense);

            return View(mapped);
        }
    }
}