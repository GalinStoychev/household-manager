using HouseholdManager.Models;
using HouseholdManager.Models.Enums;
using System;

namespace HouseholdManager.Logic.Contracts.Factories
{
    public interface IInvitationFactory
    {
        Invitation CreateInvitation(Guid householdId, string userId, Status status);
    }
}
