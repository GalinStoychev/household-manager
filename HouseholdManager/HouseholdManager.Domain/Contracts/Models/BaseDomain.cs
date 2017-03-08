using System;

namespace HouseholdManager.Domain.Contracts.Models
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; }
    }
}
