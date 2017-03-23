using HouseholdManager.Common.Constants;
using HouseholdManager.Data;
using HouseholdManager.Data.Repositories;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IHouseholdService householdService;
        private readonly IExpenseService expenseService;

        public HomeController(IUserService userService, IHouseholdService householdService, IExpenseService expenseService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "userService"));
            }

            if (householdService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "householdService"));
            }

            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            this.userService = userService;
            this.householdService = householdService;
            this.expenseService = expenseService;
        }

        public ActionResult Index()
        {
            int booster = 133;
            var model = new HomeViewModel();
            model.TotalUsers = this.userService.GetUsersCount() * booster;
            model.TotalHouseholds = this.householdService.GetHouseholdsCount() * booster;
            model.TotalExpenses = this.expenseService.GetExpensesCount() * booster;

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}