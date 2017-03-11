﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class HouseholdViewModel
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

        //public ICollection<string> Users { get; set; }

        //public ICollection<IExpense> Expenses { get; set; }
    }
}