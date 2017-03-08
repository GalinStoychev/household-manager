using HouseholdManager.Domain.Contracts.Models;
using System;

namespace HouseholdManager.Domain.Models
{
    public abstract class BaseDomain : IIdentifiable
    {
        public Guid Id { get; set; }
    }
}
