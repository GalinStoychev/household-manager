﻿using HouseholdManager.Data;
using HouseholdManager.Data.Models;
using HouseholdManager.Data.Repositories;
using HouseholdManager.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new HouseholdManagerDbContext();
            var repo = new ExpenseRepositoryEF(context);
            repo.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}