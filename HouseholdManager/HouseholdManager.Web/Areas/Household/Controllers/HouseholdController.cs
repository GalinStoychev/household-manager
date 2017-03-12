using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
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

        public HouseholdController(IUserService userService, IHouseholdService householdService, IImageService imageService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "user service"));
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
            return View();
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
        public ActionResult Create(HouseholdViewModel model)
        {
            this.householdService.CreateHousehold(model.Name, model.Address, model.Image, this.User.Identity.GetUserId());

            return RedirectToAction("SetCurrentHousehold", new { name = model.Name });
        }

        [HttpGet]
        public ActionResult SetCurrentHousehold(string name)
        {
            this.userService.SetCurrentHousehold(name, this.User.Identity.GetUserId());

            return this.RedirectToHousehold(name);
        }

        private ActionResult RedirectToHousehold(string name)
        {
            var currentHousehold = this.userService.GetCurrentHousehold(this.User.Identity.GetUserId());
            this.SetHouseholdCookie(currentHousehold?.Name, currentHousehold?.Id.ToString());

            return this.RedirectToRoute("Household_single", new { name = name });
        }

        private void SetHouseholdCookie(string name, string id)
        {
            var cookie = new HttpCookie(CommonConstants.CurrentHousehold);
            cookie.Values.Add(CommonConstants.CurrentHouseholdName, name);
            cookie.Values.Add(CommonConstants.CurrentHouseholdId, id);
            this.HttpContext.Response.Cookies.Set(cookie);
        }
    }
}