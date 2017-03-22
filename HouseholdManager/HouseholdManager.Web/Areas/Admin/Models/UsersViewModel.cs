using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Web.Areas.Admin.Models
{
    public class UsersViewModel :IMapFrom<User>
    {
        [Editable(false)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Editable(false)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool Admin { get; set; }

        public bool IsDeleted { get; set; }
    }
}