using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class ExpenseCategory: BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
