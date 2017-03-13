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
        public Guid  Id { get; set; }

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
                .ForMember(s => s.AssignedUser, opt => opt.MapFrom(d => d.AssignedUser.FirstName + " " + d.AssignedUser.FirstName))
                .ForMember(s => s.PaidBy, opt => opt.MapFrom(d => d.PaidBy.FirstName + " " + d.PaidBy.FirstName));
        }
    }
}