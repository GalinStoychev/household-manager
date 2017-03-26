using Microsoft.AspNet.SignalR;
using HouseholdManager.Logic.Services;
using HouseholdManager.Data.Repositories;
using HouseholdManager.Models;
using HouseholdManager.Data;
using HouseholdManager.Web.WebHelpers;

namespace HouseholdManager.Web.Hubs
{
    public class Notification : Hub
    {
        public void SendNotification(string username)
        {
            // send username and householdId to db to add to Invitation Table

            var context = new HouseholdManagerDbContext();
            var invitationservie = new InvitationService(new UnitOfWork(context), new GenericRepositoryEF<Invitation>(context), new GenericRepositoryEF<User>(context));
            var webHelper = new WebHelper();


            var connId = Context.ConnectionId;
            Clients.User(username).addNotification();
            Clients.Client(connId).addMessage("Invitation was sent");

            //if (username == webHelper.GetUserName())
            //{
            //    Clients.Client(connId).reject("You cannot sent invitation to yourself!");
            //}
            //else
            //{
            //    var isSucces = invitationservie.AddInvitation(username, webHelper.GetHouseholdIdFromCookie());

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