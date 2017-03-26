using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Models;
using HouseholdManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdManager.Logic.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Invitation> invitationRepositoryEF;
        private readonly IRepository<User> userRepositoryEF;
        private readonly IInvitationFactory invitationFactory;

        public InvitationService(IUnitOfWork unitOfWork, IRepository<Invitation> invitationRepositoryEF, IRepository<User> userRepositoryEF)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "unitOfWork"));
            }

            if (invitationRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "invitationRepositoryEF"));
            }

            if (userRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "userRepositoryEF"));
            }

            //if (invitationFactory == null)
            //{
            //    throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "invitationFactory"));
            //}

            this.unitOfWork = unitOfWork;
            this.invitationRepositoryEF = invitationRepositoryEF;
            this.userRepositoryEF = userRepositoryEF;
            //this.invitationFactory = invitationFactory;
        }

        public void AcceptInvitation(Guid invitationId)
        {
            var invitation = this.invitationRepositoryEF.GetFirst(
                x => x.Id == invitationId,
                x => x.Household, x => x.User);

            invitation.Status = Status.Accepted;
            this.invitationRepositoryEF.Update(invitation);

            var user = invitation.User;
            var household = invitation.Household;
            user.Households.Add(household);
            this.userRepositoryEF.Update(user);

            this.unitOfWork.Commit();
        }

        public bool AddInvitation(string username, Guid householdId)
        {
            var user = this.userRepositoryEF.GetFirst(x => x.UserName == username);

            var doesExist = this.invitationRepositoryEF.GetAll<Invitation>(x => x.UserId == user.Id && x.HouseholdId == householdId, null);
            if (doesExist.Count() > 0)
            {
                return false;
            }

            //var invitation = this.invitationFactory.CreateInvitation(householdId, user.Id, Status.Waiting);
            var invitation = new Invitation(householdId, user.Id, Status.Waiting);

            this.invitationRepositoryEF.Add(invitation);
            this.unitOfWork.Commit();

            return true;
        }

        public IEnumerable<Invitation> GetUserInvitations(string userId)
        {
            var invitations = this.invitationRepositoryEF.GetAll<Invitation>(
                x => x.UserId == userId && x.Status == Status.Waiting,
                null,
                x => x.Household);

            return invitations;
        }
    }
}
