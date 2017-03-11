using System;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;
using HouseholdManager.Models;

namespace HouseholdManager.Logic.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Household> householdRepositoryEF;

        public HouseholdService(IUnitOfWork unitOfWork, IRepository<Household> householdRepositoryEF)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, unitOfWork));
            }

            if (householdRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, householdRepositoryEF));
            }

            this.unitOfWork = unitOfWork;
            this.householdRepositoryEF = householdRepositoryEF;
        }

        public void CraeteHousehold(string name, string address, byte[] image)
        {
            throw new NotImplementedException();
        }

        public Household GetHousehold(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
