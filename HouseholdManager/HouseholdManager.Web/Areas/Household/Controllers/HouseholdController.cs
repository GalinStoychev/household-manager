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
    public class HouseholdController : BaseController
    {
        private readonly IUserService userService;
        private readonly IHouseholdService householdService;
        private readonly IImageService imageService;

        public HouseholdController(IUserService userService, IHouseholdService householdService, IImageService imageService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "userService"));
            }

            if (householdService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "householdService"));
            }

            if (imageService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "imageService"));
            }
            
            this.userService = userService;
            this.householdService = householdService;
            this.imageService = imageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var householdId = this.webHelper.GetHouseholdIdFromCookie();
            var household = this.householdService.GetHousehold(householdId);

            var model = new HouseholdViewModel();
            mappingService.Map<HouseholdManager.Models.Household, HouseholdViewModel>(household, model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new HouseholdViewModel()
            {
                Image = (byte[])this.TempData["image"] ?? this.imageService.LoadHouseholdDefaultImage(),
                ImageUploadMessage = (string)this.TempData[CommonConstants.UploadMessage]
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HouseholdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Error/BadRequest");
            }

            this.householdService.CreateHousehold(model.Name, model.Address, model.Image, this.webHelper.GetUserId());

            return RedirectToAction("SetCurrentHousehold", new { name = model.Name });
        }

        [HttpGet]
        public ActionResult SetCurrentHousehold(string name)
        {
            this.userService.SetCurrentHousehold(name, this.webHelper.GetUserId());

            return this.RedirectToHousehold(name);
        }
        
        private ActionResult RedirectToHousehold(string name)
        {
            var currentHousehold = this.userService.GetCurrentHousehold(this.webHelper.GetUserName());
            this.webHelper.SetHouseholdCookie(currentHousehold?.Name, currentHousehold?.Id.ToString());

            return this.RedirectToRoute("Household_single", new { name = name });
        }
    }
}