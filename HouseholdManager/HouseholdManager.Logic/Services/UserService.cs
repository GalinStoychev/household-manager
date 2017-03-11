using HouseholdManager.Common.Constants;
using HouseholdManager.Domain.Contracts;
using HouseholdManager.Domain.Contracts.Models;
using HouseholdManager.Domain.Contracts.Repositories;
using HouseholdManager.Domain.Models;
using HouseholdManager.Logic.Contracts;
using System;

namespace HouseholdManager.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepositoryEF userRepositoryEF;

        public UserService(IUnitOfWork unitOfWork, IUserRepositoryEF userRepositoryEF)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, unitOfWork));
            }

            if (userRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, userRepositoryEF));
            }

            this.unitOfWork = unitOfWork;
            this.userRepositoryEF = userRepositoryEF;
        }

        public void AddHousehold(string name, string address, byte[] image, string username)
        {
            var user = this.userRepositoryEF.GetByUsername(username);
            var household = new Household(name, address, image);
            user.Households.Add(household);

            this.userRepositoryEF.Update(user);
            this.unitOfWork.Commit();
        }

        public IUser GetUserInfo(string username)
        {
            var user = this.userRepositoryEF.GetByUsername(username);
            if (user == null)
            {
                throw new ArgumentNullException(ExceptionConstants.UserWasNotFound);
            }

            return user;
        }

        public void SetCurrentHousehold(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
