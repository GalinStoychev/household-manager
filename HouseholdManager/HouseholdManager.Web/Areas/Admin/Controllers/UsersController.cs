using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Admin.Models;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.Controllers;
using HouseholdManager.Web.WebHelpers.Contracts;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace HouseholdManager.Web.Areas.Admin.Controllers
{

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(String.Format(ExceptionConstants.ArgumentCannotBeNull, "userService"));
            }

            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = this.userService.GetAll();
            var model = new List<UsersViewModel>();
            foreach (var user in users)
            {
                var modelUser = this.mappingService.Map<UsersViewModel>(user);
                if (user.Roles.Any(x => x.RoleId == "2"))
                {
                    modelUser.Admin = true;
                }

                model.Add(modelUser);
            }

            return View("UsersGrid", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([Bind(Exclude = "Email")] UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.webHelper.ChangeAdminRole(model.Id, model.Admin);

                this.userService.Delete(model.Id, model.IsDeleted);

                this.userService.UpdateUserInfo(model.Id, model.FirstName, model.LastName, model.PhoneNumber);

                RouteValueDictionary routeValues = this.GridRouteValues();

                return RedirectToAction("UsersGrid", routeValues);
            }

            return View("UsersGrid");
        }

        [HttpGet]
        public ActionResult ShowHouseholds(UsersViewModel model)
        {
            var users = this.userService.GetUserInfo(model.Id);
            var modelHouseholds = new List<HouseholdsViewModel>();
            foreach (var household in users.Households)
            {
                var modelHousehold = this.mappingService.Map<HouseholdsViewModel>(household);
                modelHouseholds.Add(modelHousehold);
            }

            return this.View("HouseholdsGrid", modelHouseholds);
        }
    }
}