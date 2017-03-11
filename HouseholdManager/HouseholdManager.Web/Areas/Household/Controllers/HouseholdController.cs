using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    public class HouseholdController : Controller
    {
        private readonly IUserService userService;

        public HouseholdController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "user service"));
            }

            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new HouseholdViewModel()
            {
                Image = (byte[])this.TempData["image"],
                ImageUploadMessage = (string)this.TempData[CommonConstants.UploadMessage]
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HouseholdViewModel model)
        {
            // add household via hhservice or via userservice
            this.userService.AddHousehold(model.Name, model.Address, model.Image, this.User.Identity.Name);

            return RedirectToAction("Index");
        }
    }
}