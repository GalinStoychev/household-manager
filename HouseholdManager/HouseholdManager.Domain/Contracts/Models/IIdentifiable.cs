using System;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
