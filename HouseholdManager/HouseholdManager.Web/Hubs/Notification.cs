using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.WebHelpers.Contracts;
using Microsoft.AspNet.SignalR;
using System;

namespace HouseholdManager.Web.Hubs
{
    public class Notification : Hub
    {
        private readonly IInvitationService invitationService;
        private readonly IWebHelper webHelper;

        public Notification(IInvitationService invitationService, IWebHelper webHelper)
        {
            if (invitationService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "invitationService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.invitationService = invitationService;
            this.webHelper = webHelper;
        }

        public void SendNotification(string username)
        {
            var connId = Context.ConnectionId;
            Clients.User(username).addNotification();
            Clients.Client(connId).addMessage("Invitation was sent");

            //if (username == webHelper.GetUserName())
            //{
            //    Clients.Client(connId).reject("You cannot sent invitation to yourself!");
            //}
            //else
            //{
            //    var isSucces =  this.invitationService.AddInvitation(username, webHelper.GetHouseholdIdFromCookie());

            //    if (isSucces)
            //    {
            //        Clients.User(username).addNotification();
            //        Clients.Client(connId).addMessage("Invitation was sent");
            //    }
            //    else
            //    {
            //        Clients.Client(connId).addMessage("Invitation was previously sent!");
            //    }
            //}

            //Clients.User(this.Context.User.Identity.GetUserId()).addNotification(connId);

            //  Clients.Client(connId).addNotification(message + " " + connId);
        }
    }
}