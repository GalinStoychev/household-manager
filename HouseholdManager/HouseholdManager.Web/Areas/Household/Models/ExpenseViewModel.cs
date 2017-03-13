using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ExpenseViewModel : IMapFrom<Expense>, IHaveCustomMappings
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        public string Category { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage =" Expected cost is required.")]
        [Display(Name = "Expected cost")]
        public decimal ExpectedCost { get; set; }

        public decimal Cost { get; set; }

        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Paid on date")]
        public DateTime PaidOnDate { get; set; }

        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Is paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Paid by")]
        public string PaidBy { get; set; }

        [Display(Name = "Assigned user")]
        public string AssignedUser { get; set; }

        public IList<SelectListItem> Users { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be maximum 100 characters long.")]
        public string Comment { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Expense, ExpenseViewModel>()
                .ForMember(s => s.AssignedUser, opt => opt.MapFrom(d => d.AssignedUser.FirstName + " " + d.AssignedUser.FirstName))
                .ForMember(s => s.PaidBy, opt => opt.MapFrom(d => d.PaidBy.FirstName + " " + d.PaidBy.FirstName));
        }
    }
}