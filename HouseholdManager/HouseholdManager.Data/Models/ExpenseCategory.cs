﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Data.Models
{
    public class ExpenseCategory: BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
