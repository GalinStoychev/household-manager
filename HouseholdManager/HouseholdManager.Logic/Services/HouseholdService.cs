using System;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;
using HouseholdManager.Models;
using HouseholdManager.Logic.Contracts.Factories;

namespace HouseholdManager.Logic.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Household> householdRepositoryEF;
        private readonly IHouseholdFactory householdFactory;

        public HouseholdService(IUnitOfWork unitOfWork, IRepository<Household> householdRepositoryEF, IHouseholdFactory householdFactory)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, unitOfWork));
            }

            if (householdRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, householdRepositoryEF));
            }

            if (householdFactory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, householdFactory));
            }

            this.unitOfWork = unitOfWork;
            this.householdRepositoryEF = householdRepositoryEF;
            this.householdFactory = householdFactory;
        }

        public Household CreateHousehold(string name, string address, byte[] image)
        {
            var household = this.householdFactory.CreateHousehold(name, address, image);
            this.householdRepositoryEF.Add(household);
            this.unitOfWork.Commit();

            return household;
        }

        public Household GetHousehold(Guid id)
        {
            var household = this.householdRepositoryEF.GetById(id);
            return household;
        }
    }
}
