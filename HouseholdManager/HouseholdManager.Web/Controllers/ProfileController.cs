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
        private readonly IInvitationService invitationService;

        public ProfileController(IUserService userService, IInvitationService invitationService, IMapingService mappingService, IWebHelper webHelper)
            : base(mappingService, webHelper)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "userService"));
            }

            if (invitationService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "invitationService"));
            }

            this.userService = userService;
            this.invitationService = invitationService;
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

            var invitations = this.invitationService.GetUserInvitations(user.Id);
            profileUser.Invitations= new Dictionary<string, Guid>();
            foreach (var invitation in invitations)
            {
                profileUser.Invitations.Add(invitation.Household.Name, invitation.Id);
            }

            return View(profileUser);
        }

        public ActionResult AcceptInvitation(Guid invitationId, string household)
        {
            var user = this.webHelper.GetUserName();
            this.invitationService.AcceptInvitation(invitationId);

            this.userService.SetCurrentHousehold(household, this.webHelper.GetUserId());
            var currentHousehold = this.userService.GetCurrentHousehold(this.webHelper.GetUserName());
            this.webHelper.SetHouseholdCookie(household, currentHousehold.Id.ToString());

            return RedirectToAction("Index");
        }
    }
}