using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapingService mapingService;
        private readonly IWebHelper webHelper;

        public ProfileController(IUserService userService, IMapingService mapingService, IWebHelper webHelper)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "user service"));
            }

            if (mapingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mapingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.userService = userService;
            this.mapingService = mapingService;
            this.webHelper = webHelper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var user = this.userService.GetUserInfo(this.webHelper.GetUserId());
            var profileUser =  this.mapingService.Map<ProfileViewModel>(user);
            profileUser.Households = new List<string>();
            foreach (var household in user.Households)
            {
                profileUser.Households.Add(household.Name);
            }

            return View(profileUser);
        }
    }
}