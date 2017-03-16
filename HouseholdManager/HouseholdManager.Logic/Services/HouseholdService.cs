using System;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;
using HouseholdManager.Models;
using HouseholdManager.Logic.Contracts.Factories;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Household> householdRepositoryEF;
        private readonly IRepository<User> userRepositoryEF;
        private readonly IHouseholdFactory householdFactory;

        public HouseholdService(IUnitOfWork unitOfWork, IRepository<Household> householdRepositoryEF, IRepository<User> userRepositoryEF, IHouseholdFactory householdFactory)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, unitOfWork));
            }

            if (householdRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, householdRepositoryEF));
            }

            if (userRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, userRepositoryEF));
            }

            if (householdFactory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, householdFactory));
            }

            this.unitOfWork = unitOfWork;
            this.householdRepositoryEF = householdRepositoryEF;
            this.userRepositoryEF = userRepositoryEF;
            this.householdFactory = householdFactory;
        }

        public void CreateHousehold(string name, string address, byte[] image, string userId)
        {
            var household = this.householdFactory.CreateHousehold(name, address, image);
            var user = this.userRepositoryEF.GetById(userId);
            household.Users.Add(user);
            this.householdRepositoryEF.Add(household);
            this.unitOfWork.Commit();
        }

        public Household GetHousehold(Guid id)
        {
            var household = this.householdRepositoryEF.GetFirst(x => x.Id == id, x => x.Users);
            return household;
        }

        public IEnumerable<User> GetHouseholdUsers(Guid id)
        {
            var users = this.GetHousehold(id).Users;
            return users;
        }
    }
}
