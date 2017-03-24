using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Models
{
    public class TotalMonthlyExpencesViewModel
    {
        public decimal Total { get; set; }

        public IDictionary<string, decimal> MoneyPaid { get; set; }

        public IDictionary<string, decimal> MoneyResult { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public IList<SelectListItem> Years { get; set; }

        public IList<SelectListItem> Months { get; set; }
    }
}