using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using System;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ExpenseCategoryViewModel: IMapFrom<ExpenseCategory>
    {
        public string Name { get; set; }

        public Guid Id { get; set; }
    }
}