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
    public class ProfileController : BaseController
    {
        private readonly IUserService userService;

        public ProfileController(IUserService userService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "user service"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mapingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var user = this.userService.GetUserInfo(this.webHelper.GetUserId());
            var profileUser =  this.mappingService.Map<ProfileViewModel>(user);
            profileUser.Households = new List<string>();
            foreach (var household in user.Households)
            {
                profileUser.Households.Add(household.Name);
            }

            return View(profileUser);
        }
    }
}