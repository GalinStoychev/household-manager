using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    public class HouseholdController : Controller
    {
        private readonly IUserService userService;
        private readonly IHouseholdService householdService;
        private readonly IImageService imageService;
        private readonly IMapingService mappingService;
        private readonly IWebHelper webHelper;

        public HouseholdController(IUserService userService, IHouseholdService householdService, IImageService imageService, IMapingService mappingService, IWebHelper webHelper)
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

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.userService = userService;
            this.householdService = householdService;
            this.imageService = imageService;
            this.mappingService = mappingService;
            this.webHelper = webHelper;
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
            var currentHousehold = this.userService.GetCurrentHousehold(this.webHelper.GetUserId());
            this.webHelper.SetHouseholdCookie(currentHousehold?.Name, currentHousehold?.Id.ToString());

            return this.RedirectToRoute("Household_single", new { name = name });
        }
    }
}