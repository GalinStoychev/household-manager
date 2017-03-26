using HouseholdManager.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Invitation : BaseEntity
    {
        private Invitation()
        {
        }

        public Invitation(Guid householdId, string userId, Status status)
        {
            this.HouseholdId = householdId;
            this.UserId = userId;
            this.Status = status;
        }

        [ForeignKey("Household")]
        public Guid HouseholdId { get; protected set; }

        public virtual Household Household { get; protected set; }

        [ForeignKey("User")]
        public string UserId { get; protected set; }

        public virtual User User { get; set; }

        public Status Status { get; set; }
    }
}
