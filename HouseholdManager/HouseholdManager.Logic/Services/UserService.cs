using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<User> userRepositoryEF;

        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepositoryEF)
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
            throw new NotImplementedException();
        }

        public User GetUserInfo(string id)
        {
            var user = this.userRepositoryEF.GetById(id);
            //if (user == null)
            //{
            //    throw new ArgumentNullException(ExceptionConstants.UserWasNotFound);
            //}

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
