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
            var msg = string.Empty;

            if (username == webHelper.GetUserName())
            {
                msg = "You cannot sent invitation to yourself!";
            }
            else
            {
                var isSucces = this.invitationService.AddInvitation(username, webHelper.GetHouseholdIdFromCookie());

                if (isSucces)
                {
                    Clients.User(username).addNotification();
                    msg = "Invitation was sent.";
                }
                else
                {
                    msg = "Invitation was previously sent or user is already in this household!";
                }
            }

            var connId = Context.ConnectionId;
            Clients.Client(connId).addMessage(msg);
        }
    }
}