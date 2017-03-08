namespace HouseholdManager.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
