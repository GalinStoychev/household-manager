using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class CommentViewModel :IMapFrom<Comment>, IHaveCustomMappings
    {
        public string CommentContent { get; set; }

        public string User { get; set; }

        public DateTime CreateOnDate { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(s => s.User, opt => opt.MapFrom(d => d.User.FirstName + " " + d.User.LastName));
        }
    }
}