using HouseholdManager.Common.Contracts;
using HouseholdManager.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using HouseholdManager.Models;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class HouseholdViewModel : IMapFrom<HouseholdManager.Models.Household>, IHaveCustomMappings
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Household name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public byte[] Image { get; set; }

        public string ImageUploadMessage { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Address { get; set; }

        public ICollection<ProfileViewModel> Users { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<HouseholdManager.Models.Household, HouseholdViewModel>();
        }

        //public ICollection<IExpense> Expenses { get; set; }
    }
}