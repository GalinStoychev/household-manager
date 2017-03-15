using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ShowExpenseViewModel : BaseExpenseViewModel, IMapFrom<Expense>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The cost cannot be a negative number.")]
        public decimal Cost { get; set; }

        [Display(Name = "Paid on date")]
        public DateTime PaidOnDate { get; set; }

        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Is paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Paid by")]
        public string PaidBy { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Expense, ShowExpenseViewModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.ExpenseCategory.Name))
                .ForMember(d => d.AssignedUser, opt => opt.MapFrom(s => s.AssignedUser.FirstName + " " + s.AssignedUser.LastName))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.LastName))
                .ForMember(d => d.PaidBy, opt => opt.MapFrom(s => s.PaidBy.FirstName + " " + s.PaidBy.LastName));
        }
    }
}