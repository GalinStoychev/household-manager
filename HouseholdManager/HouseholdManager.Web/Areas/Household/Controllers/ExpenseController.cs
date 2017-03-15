using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using System;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly IMapingService mappingService;

        public ExpenseController(IExpenseService expenseService, IMapingService mappingService)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            this.expenseService = expenseService;
            this.mappingService = mappingService;
        }
      
        [HttpGet]
        public ActionResult Index(string id)
        {
            var expense = this.expenseService.GetExpense(Guid.Parse(id));
            var mapped = this.mappingService.Map<ShowExpenseViewModel>(expense);

            return View(mapped);
        }
    }
}