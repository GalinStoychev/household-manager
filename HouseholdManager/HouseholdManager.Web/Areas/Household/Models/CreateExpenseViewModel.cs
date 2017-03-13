using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class CreateExpenseViewModel : BaseExpenseViewModel, IMapFrom<Expense>, IHaveCustomMappings
    {
        public IList<SelectListItem> Categories { get; set; }
        
        public IList<SelectListItem> Users { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be maximum 100 characters long.")]
        public string Comment { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Expense, CreateExpenseViewModel>()
                .ForMember(s => s.AssignedUser, opt => opt.MapFrom(d => d.AssignedUser.FirstName + " " + d.AssignedUser.FirstName));
        }
    }
}