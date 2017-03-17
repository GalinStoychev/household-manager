using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly IMapingService mappingService;
        private readonly IWebHelper webHelper;

        public ExpensesController(IExpenseService expenseService, IMapingService mappingService, IWebHelper webHelper)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.expenseService = expenseService;
            this.mappingService = mappingService;
            this.webHelper = webHelper;
        }

        [HttpGet]
        public ActionResult Index(string name, string search = "", bool isPaid = false, int page = 1)
        {
            var model = new ShowExpensesViewModel();
            model.SearchPattern = search;

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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchViewModel model)
        {
            return RedirectToAction("Index", "Expenses", new {name = this.webHelper.GetHouseholdNameFromCookie(), search = model.SearchPattern });
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