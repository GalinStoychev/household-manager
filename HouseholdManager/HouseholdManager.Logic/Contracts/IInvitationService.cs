using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IInvitationService
    {
        IEnumerable<Invitation> GetUserInvitations(string userId);

        bool AddInvitation(string username, Guid householdId);

        void AcceptInvitation(Guid invitationId);
    }
}
