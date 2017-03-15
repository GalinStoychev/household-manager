using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class CreateExpenseViewModel : BaseExpenseViewModel, IMapFrom<Expense>, IHaveCustomMappings
    {
        public IList<SelectListItem> Categories { get; set; }
        
        public IList<SelectListItem> Users { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Expense, CreateExpenseViewModel>()
                .ForMember(s => s.AssignedUser, opt => opt.MapFrom(d => d.AssignedUser.FirstName + " " + d.AssignedUser.FirstName));
        }
    }
}