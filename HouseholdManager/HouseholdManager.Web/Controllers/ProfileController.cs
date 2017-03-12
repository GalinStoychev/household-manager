using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapingService mapingService;

        public ProfileController(IUserService userService, IMapingService mapingService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "user service"));
            }

            if (mapingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mapingService"));
            }

            this.userService = userService;
            this.mapingService = mapingService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var user = this.userService.GetUserInfo(this.User.Identity.GetUserId());
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