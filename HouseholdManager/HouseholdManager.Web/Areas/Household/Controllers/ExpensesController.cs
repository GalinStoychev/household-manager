using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Household/Expense
        public ActionResult Index()
        {
            return View();
        }
    }
}