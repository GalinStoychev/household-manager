﻿using AutoMapper;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ExpenseViewModel: IMapFrom<Expense>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be between {2} and 30 characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        public string Category { get; set; }

        [Display(Name = "Assigned user")]
        public string AssignedUser { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = " Expected cost is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The expected cost cannot be a negative number.")]
        [Display(Name = "Expected cost")]
        public decimal ExpectedCost { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be maximum 200 characters long.")]
        public string Comment { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The cost cannot be a negative number.")]
        [Display(Name = "Paid cost")]
        public decimal? Cost { get; set; }

        [Display(Name = "Paid on date")]
        public DateTime? PaidOnDate { get; set; }

        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Is paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Paid by")]
        public string PaidBy { get; set; }

        public IList<SelectListItem> CategoriesList { get; set; }

        public IList<SelectListItem> Users { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Expense, ExpenseViewModel>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.ExpenseCategory.Name))
                .ForMember(d => d.AssignedUser, opt => opt.MapFrom(s => s.AssignedUser.FirstName + " " + s.AssignedUser.LastName))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.LastName))
                .ForMember(d => d.PaidBy, opt => opt.MapFrom(s => s.PaidBy.FirstName + " " + s.PaidBy.LastName));
        }
    }
}