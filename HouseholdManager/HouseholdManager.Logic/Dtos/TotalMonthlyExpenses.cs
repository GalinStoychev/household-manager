using System.Collections.Generic;

namespace HouseholdManager.Logic.Dtos
{
    public class TotalMonthlyExpenses
    {
        public TotalMonthlyExpenses()
        {
            this.MoneyPaid = new Dictionary<string, decimal>();
            this.MoneyResult = new Dictionary<string, decimal>();
        }

        public decimal Total { get; set; }

        public IDictionary<string, decimal> MoneyPaid { get; set; }

        public IDictionary<string, decimal> MoneyResult { get; set; }
    }
}
