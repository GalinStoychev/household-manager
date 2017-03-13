using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class BaseExpenseViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        public string Category { get; set; }

        [Display(Name = "Assigned user")]
        public string AssignedUser { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = " Expected cost is required.")]
        [Display(Name = "Expected cost")]
        public decimal ExpectedCost { get; set; }
    }
}