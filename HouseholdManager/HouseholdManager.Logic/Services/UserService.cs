using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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

        public void AddHousehold(Household household, string id)
        {
            var user = this.userRepositoryEF.GetById(id);
            user.Households.Add(household);
            if (user.CurrentHousehold == null)
            {
                user.SetCurrentHousehold(household);
            }

            this.unitOfWork.Commit();
        }

        public IEnumerable<User> GetAll()
        {
            var users = this.userRepositoryEF.GetAll<User>(null, null, x => x.Roles);
            return users;
        }

        public Household GetCurrentHousehold(string email)
        {
            var currentHousehold = this.userRepositoryEF.GetFirst(x => x.Email == email).CurrentHousehold;
            return currentHousehold;
        }

        public User GetUserInfo(string id)
        {
            var user = this.userRepositoryEF.GetById(id);

            return user;
        }

        public void SetCurrentHousehold(Guid householdId, string userId)
        {
            var user = this.userRepositoryEF.GetById(userId);
            var household = user.Households.Where(x => x.Id == householdId).FirstOrDefault();
            if (household != null)
            {
                user.SetCurrentHousehold(household);
                this.unitOfWork.Commit();
            }
        }

        public void SetCurrentHousehold(string householdName, string userId)
        {
            var user = this.userRepositoryEF.GetById(userId);
            var household = user.Households.Where(x => x.Name == householdName).FirstOrDefault();
            if (household != null)
            {
                user.SetCurrentHousehold(household);
                this.unitOfWork.Commit();
            }
        }

        public void UpdateUserInfo(string id, string firstName, string lastName, string phoneNumber)
        {
            var user = this.userRepositoryEF.GetById(id);
            user.Update(firstName, lastName, phoneNumber);

            this.userRepositoryEF.Update(user);
            this.unitOfWork.Commit();
        }

        public void Delete(string id, bool isDeleted)
        {
            var user = this.userRepositoryEF.GetById(id);
            user.Delete(isDeleted);

            this.userRepositoryEF.Update(user);
            this.unitOfWork.Commit();
        }
    }
}
