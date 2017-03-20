using System.Collections.Generic;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class ShowExpensesViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }

        public string SearchPattern { get; set; }

        public int NextPage { get; set; }

        public int PrevousPage { get; set; }

        public double PagesCount { get; set; }

        public bool IsPaid { get; set; }
    }
}