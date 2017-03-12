using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ExpenseViewModel : IMapFrom<Expense>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public decimal ExpectedCost { get; set; }

        public decimal Cost { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PaidOnDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPaid { get; set; }

        public string AssignedUser { get; set; }

        public string PaidBy { get; set; }

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