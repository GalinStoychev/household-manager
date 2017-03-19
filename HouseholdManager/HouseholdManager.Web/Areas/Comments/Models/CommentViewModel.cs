using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Web.Areas.Comments.Models
{
    public class CommentViewModel :IMapFrom<Comment>, IHaveCustomMappings
    {
        public Guid ExpenceId { get; set; }

        [Required]
        [Display(Name ="Content")]
        [StringLength(100, ErrorMessage = "The comment must be between {0} and 100 characters long", MinimumLength = 2)]
        public string CommentContent { get; set; }

        public string User { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(s => s.User, opt => opt.MapFrom(d => d.User.FirstName + " " + d.User.LastName));
        }
    }
}