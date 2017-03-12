using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System.Collections.Generic;

namespace HouseholdManager.Web.Models
{
    public class ProfileViewModel: IMapFrom<User>, IHaveCustomMappings
    {
        public string FullName { get; set; }

        public string  Email { get; set; }

        public string PhoneNumber { get; set; }

        public IList<string> Households { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, ProfileViewModel>()
              .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
              .ForMember(d => d.Households, opt => opt.Ignore());
        }
    }
}