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
    public class ExpensesController : BaseController
    {
        private const string IsHistory = "IsHistory";

        private readonly IExpenseService expenseService;

        public ExpensesController(IExpenseService expenseService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            this.expenseService = expenseService;
        }

        [HttpGet]
        public ActionResult Index(string name, string search = "", bool isPaid = false, int page = 1)
        {
            this.TempData[IsHistory] = false;
            var model = this.GetExpenses(false, search, page);
            return View(model);
        }

        [HttpGet]
        public ActionResult History(string search = "", int page = 1)
        {
            this.TempData[IsHistory] = true;
            var model = this.GetExpenses(true, search, page);
            return View("Index", model);
        }

        private ShowExpensesViewModel GetExpenses(bool isPaid, string search, int page)
        {
            var model = new ShowExpensesViewModel();
            model.SearchPattern = search;
            model.IsPaid = isPaid;

            var householdId = this.webHelper.GetHouseholdIdFromCookie();
            var expensesCount = this.expenseService.GetExpensesCount(householdId, isPaid, search);
            model.PagesCount = Math.Ceiling((double)expensesCount / CommonConstants.DefaultPageSize);

            if (page < CommonConstants.DefaultStartingPage)
            {
                model.PrevousPage = CommonConstants.DefaultStartingPage;
                model.NextPage = CommonConstants.DefaultStartingPage + 1;
            }
            else
            {
                model.PrevousPage = page - 1;
                model.NextPage = page + 1;
            }

            var expenses = this.expenseService.GetExpenses(householdId, page, isPaid, search);
            var modelExpenses = new List<ShowExpenseViewModel>();
            foreach (var expense in expenses)
            {
                var mapped = this.mappingService.Map<ShowExpenseViewModel>(expense);
                modelExpenses.Add(mapped);
            }

            model.Expenses = modelExpenses;

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchViewModel model)
        {
            var action = "Index";
            if ((bool)this.TempData[IsHistory] == true)
            {
                action = "History";
            }

            return RedirectToAction(action, "Expenses", new
            {
                name = this.webHelper.GetHouseholdNameFromCookie(),
                search = model.SearchPattern
            });
        }

        [ChildActionOnly]
        public ActionResult LoadSearchForm()
        {
            var model = new SearchViewModel() { ActionName = "Search", ControllerName = "Expenses" };
            return View("_SearchPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "Cost, Id, Comment, Name")] ShowExpenseViewModel model)
        {
            this.expenseService.Pay(model.Id, this.webHelper.GetUserId(), model.Comment, (decimal)model.Cost);
            return RedirectToRoute("Household_expenses", new { name = model.Name });
        }
    }
}