using HouseholdManager.Data.Contracts;
using HouseholdManager.Domain.Contracts;

namespace HouseholdManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IHouseholdManagerDbContext context;

        public UnitOfWork(IHouseholdManagerDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
