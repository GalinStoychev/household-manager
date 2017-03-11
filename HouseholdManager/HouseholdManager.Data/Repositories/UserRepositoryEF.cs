using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Models;
using HouseholdManager.Domain.Contracts.Models;
using HouseholdManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HouseholdManager.Data.Repositories
{
    public class UserRepositoryEF : IUserRepositoryEF
    {
        private readonly IEntityToDomainMapper entityToDomainMapper;

        public UserRepositoryEF(IHouseholdManagerDbContext context, IEntityToDomainMapper entityToDomainMapper)
        {
            if (context == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ARGUMENT_CANNOT_BE_NULL, context));
            }

            if (entityToDomainMapper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ARGUMENT_CANNOT_BE_NULL, entityToDomainMapper));
            }

            this.Context = context;
            this.entityToDomainMapper = entityToDomainMapper;
            this.DbSet = this.Context.Set<User>();
        }

        protected IHouseholdManagerDbContext Context { get; }

        protected DbSet<User> DbSet { get; set; }

        public IEnumerable<IUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public IUser GetByUsername(string username)
        {
            var entity = this.DbSet.Where(x => x.UserName == username).FirstOrDefault();
            var mapped = this.entityToDomainMapper.MapUser(entity);
            return mapped;
        }

        public void Add(IUser model)
        {
            this.SetEntityState(model, EntityState.Added);
        }

        public void Delete(IUser model)
        {
            this.SetEntityState(model, EntityState.Deleted);
        }

        public void Update(IUser model)
        {
            this.SetEntityState(model, EntityState.Modified);
        }

        protected virtual void SetEntityState(IUser model, EntityState entityState)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model cannot be null!");
            }

            var entity = this.DbSet.Local.Where(e => e.Id == model.Id).FirstOrDefault();
            if (entity == null)
            {
                //entity = this.MapDomainToEnity(model);
            }
            else
            {
                //entity = this.MapDomainToEnity(model, entity);
            }

            this.Context.SetEntryState(entity, entityState);
        }
    }
}
