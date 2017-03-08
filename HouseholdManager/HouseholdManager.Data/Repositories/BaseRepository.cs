using AutoMapper;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Models;
using HouseholdManager.Domain.Contracts;
using HouseholdManager.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HouseholdManager.Data.Repositories
{
    public class BaseRepository<EntityType, DomainType> : IRepository<DomainType, EntityType>
        where EntityType : BaseEntity
        where DomainType : BaseDomain

    {
        public BaseRepository(IHouseholdManagerDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected IHouseholdManagerDbContext Context { get; set; }

        protected DbSet<EntityType> DbSet { get; set; }

        public DomainType GetById(object id)
        {
            var entity = this.DbSet.Find(id);
            var model = this.MapEntityToDomain(entity);
            return model;
        }

        public virtual IEnumerable<DomainType> GetAll()
        {
            var entities = this.DbSet.ToList();
            var models = this.MapEntitiesToDomains(entities);
            return models;
        }

        public IEnumerable<DomainType> GetAll<T1>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> selectExpression)
        {
            IQueryable<EntityType> entities = this.DbSet;

            if (filterExpression != null)
            {
                entities = entities.Where(filterExpression);
            }

            if (selectExpression != null)
            {
                entities.Select(selectExpression).ToList();
            }
            else
            {
                entities.OfType<T1>().ToList();
            }

            var models = this.MapEntitiesToDomains(entities);
            return models;
        }

        public IEnumerable<DomainType> GetAll<T1, T2>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> sortExpression, Expression<Func<EntityType, T2>> selectExpression)
        {
            IQueryable<EntityType> entities = this.DbSet;

            if (filterExpression != null)
            {
                entities = entities.Where(filterExpression);
            }

            if (sortExpression != null)
            {
                entities = entities.OrderBy(sortExpression);
            }

            if (selectExpression != null)
            {
                 entities.Select(selectExpression).ToList();
            }
            else
            {
                 entities.OfType<T2>().ToList();
            }

            var models = this.MapEntitiesToDomains(entities);
            return models;
        }

        public void Add(DomainType model)
        {
            this.SetEntityState(model, EntityState.Added);
        }

        public void Delete(DomainType model)
        {
            this.SetEntityState(model, EntityState.Deleted);
        }

        public void Update(DomainType model)
        {
            this.SetEntityState(model, EntityState.Modified);
        }

        protected virtual void SetEntityState(DomainType model, EntityState entityState)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model can not be null!");
            }

            var entity = this.DbSet.Local.Where(e => e.Id == model.Id).FirstOrDefault();
            if (entity == null)
            {
                entity = this.MapDomainToEnity(model);
            }
            else
            {
                entity = this.MapDomainToEnity(model, entity);
            }

            this.Context.SetEntryState(entity, entityState);
        }

        protected virtual void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config => config.CreateMap<DomainType, EntityType>());
        }

        protected virtual DomainType MapEntityToDomain(EntityType entity)
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());
            var domain = Mapper.Map<EntityType, DomainType>(entity);

            return domain;
        }

        private IEnumerable<DomainType> MapEntitiesToDomains(IEnumerable<EntityType> entities)
        {
            var models = new List<DomainType>();
            foreach (var entity in entities)
            {
                models.Add(this.MapEntityToDomain(entity));
            }

            return models;
        }

        private EntityType MapDomainToEnity(DomainType domain)
        {
            this.InitializeDomainToEnityMapper();
            var entity = Mapper.Map<DomainType, EntityType>(domain);

            return entity;
        }

        private EntityType MapDomainToEnity(DomainType domain, EntityType entity)
        {
            this.InitializeDomainToEnityMapper();
            var mapped = Mapper.Map<DomainType, EntityType>(domain, entity);

            return mapped;
        }
    }
}
