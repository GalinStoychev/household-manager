using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService userService;

        public ProfileController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var user = this.userService.GetUserInfo(this.User.Identity.Name);
            var profileUser = new ProfileViewModel();
            profileUser.FullName = user.FirstName + " " + user.LastName;
            profileUser.Email = user.Email;
            profileUser.PhoneNumber = user.PhoneNumber;
            profileUser.Households = new List<string>();
            foreach (var household in user.Households)
            {
                profileUser.Households.Add(household.Name);
            }

            return View(profileUser);
        }
    }
}