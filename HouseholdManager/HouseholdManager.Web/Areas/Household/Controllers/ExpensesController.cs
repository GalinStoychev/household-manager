using HouseholdManager.Common;
using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Web.Areas.Household.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IHouseholdService householdService;
        private readonly IExpenseService expenseService;
        private readonly MappingService mappingService;

        public ExpensesController(IExpenseService expenseService, MappingService mappingService, IHouseholdService householdService)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (householdService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "householdService"));
            }

            this.expenseService = expenseService;
            this.mappingService = mappingService;
            this.householdService = householdService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
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

            var model = new CreateExpenseViewModel() { Categories = modelCategories };

            var users = this.householdService.GetHouseholdUsers(this.GetHouseholdId());
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
        public ActionResult Create(CreateExpenseViewModel model)
        {
            this.expenseService.CreateExpense(this.User.Identity.GetUserId(), model.Name, Guid.Parse(model.Category), this.GetHouseholdId(), model.ExpectedCost, model.DueDate, model.Comment, model.AssignedUser);
           
            return View("Index");
        }

        private Guid GetHouseholdId()
        {
            var housholdId = this.HttpContext.Request.Cookies[CommonConstants.CurrentHousehold]?[CommonConstants.CurrentHouseholdId];
            return Guid.Parse(housholdId);
        }
    }
}