using System.Collections.Generic;

namespace HouseholdManager.Web.Models
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }

        public string  Email { get; set; }

        public string PhoneNumber { get; set; }

        public IList<string> Households { get; set; }
    }
}