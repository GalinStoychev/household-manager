using HouseholdManager.Domain.Contracts.Models;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Domain.Contracts.Repositories;

namespace HouseholdManager.Data.Repositories
{
    public class ExpenseRepositoryEF : BaseRepositoryEF<Models.Expense, IExpense>, IExpenseRepositoryEF
    {
        public ExpenseRepositoryEF(IHouseholdManagerDbContext context) : base(context)
        {
        }

        public override IExpense EntityToDomain(Models.Expense entity)
        {
            var household = new Domain.Models.Household(entity.Household.Name, entity.Household.Address);
            var model = new Domain.Models.Expense(entity.Name, entity.ExpenseCategory.Name, household, entity.ExpectedCost, entity.DueDate, entity.CreatedOnDate);
            model.IsDeleted = entity.IsDeleted;
            model.Cost = entity.Cost;
            model.Id = entity.Id;
            var assignedUser = new Domain.Models.User(entity.AssignedUser.FirstName, entity.AssignedUser.LastName, entity.AssignedUser.UserName, entity.AssignedUser.Email, entity.AssignedUser.PhoneNumber);
            model.AssignedUser = assignedUser;
            var userPaidBy = new Domain.Models.User(entity.PaidBy.FirstName, entity.PaidBy.LastName, entity.PaidBy.UserName, entity.PaidBy.Email, entity.PaidBy.PhoneNumber);
            if (entity.IsPaid)
            {
                model.Pay(userPaidBy, entity.PaidOnDate);
            }

            // TODO: Comments
            return model;
        }

        protected override void InitializeEntityToDomainMapper()
        {
            //Mapper.Initialize(config =>
            //{
            //    config.CreateMap<Expense, IExpense>();
            //    config.CreateMap<Household, IHousehold>();
            //    config.CreateMap<Comment, IComment>();
            //    config.CreateMap<User, IUser>();

            //}
            //);
        }
    }
}
