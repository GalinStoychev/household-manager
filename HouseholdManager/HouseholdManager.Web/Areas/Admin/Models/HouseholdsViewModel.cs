using HouseholdManager.Common.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Web.Areas.Admin.Models
{
    public class HouseholdsViewModel: IMapFrom<HouseholdManager.Models.Household>
    {
        [Editable(false)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}