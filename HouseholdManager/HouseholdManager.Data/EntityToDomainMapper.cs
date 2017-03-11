using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Contracts.Factories;
using HouseholdManager.Data.Models;
using HouseholdManager.Domain.Contracts.Models;
using System;

namespace HouseholdManager.Data
{
    public class EntityToDomainMapper : IEntityToDomainMapper
    {
        private readonly IModelsFactory modelFactory;

        public EntityToDomainMapper(IModelsFactory modelFactory)
        {
            this.modelFactory = modelFactory;
        }

        public IUser MapUser(User userToMap)
        {
            if (userToMap == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "userToMap"));
            }

            var mapped = this.modelFactory.CreateUser(userToMap.FirstName, userToMap.LastName, userToMap.UserName, userToMap.Email, userToMap.PhoneNumber);
            if (userToMap.CurrentHousehold != null)
            {
                var currentHouseHold = this.modelFactory.CreateHousehold(userToMap.CurrentHousehold.Name, userToMap.CurrentHousehold.Address);
                mapped.CurrentHousehold = currentHouseHold;
            }

            return mapped;
        }
    }
}
