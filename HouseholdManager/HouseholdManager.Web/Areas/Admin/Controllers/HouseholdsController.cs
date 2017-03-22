using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Admin.Models;
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
    public class HouseholdsController : BaseController
    {
        private readonly IHouseholdService householdService;

        public HouseholdsController(IHouseholdService householdService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (householdService == null)
            {
                throw new ArgumentNullException(String.Format(ExceptionConstants.ArgumentCannotBeNull, "householdService"));
            }

            this.householdService = householdService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var households = this.householdService.GetAll();
            var model = new List<HouseholdsViewModel>();
            foreach (var household in households)
            {
                var householdModel = this.mappingService.Map<HouseholdsViewModel>(household);
                model.Add(householdModel);
            }

            return View("HouseholdsGrid", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(HouseholdsViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.householdService.Delete(model.Id, model.IsDeleted);

                this.householdService.UpdateHouseholdInfo(model.Id, model.Name, model.Address);

                RouteValueDictionary routeValues = this.GridRouteValues();

                return RedirectToAction("HouseholdsGrid", routeValues);
            }

            return View("HouseholdsGrid");
        }

        [HttpGet]
        public ActionResult ShowUsers(HouseholdsViewModel model)
        {
            var users = this.householdService.GetHouseholdUsers(model.Id);
            var modelUsers = new List<UsersViewModel>();
            foreach (var user in users)
            {
                var modelUser = this.mappingService.Map<UsersViewModel>(user);
                if (user.Roles.Any(x => x.RoleId == "2"))
                {
                    modelUser.Admin = true;
                }

                modelUsers.Add(modelUser);
            }

            return this.View("UsersGrid", modelUsers);
        }
    }
}