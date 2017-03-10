using AutoMapper;
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
    public abstract class BaseRepositoryEF<EntityType, DomainType> : IRepository<DomainType>
        where EntityType : BaseEntity
        where DomainType : IIdentifiable

    {
        public BaseRepositoryEF(IHouseholdManagerDbContext context)
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
            var e = this.DbSet;
            var entities = this.DbSet
                .Include("Comments")
                .Include("ExpenseCategory")
                .Include("Household")
                .Include("AssignedUser")
                .Include("PaidBy")
                .ToList();
            //var models = this.MapEntitiesToDomains(entities);
            var models = this.EntitiesToDomains(entities);
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

        public abstract DomainType EntityToDomain(EntityType entity);

        private IEnumerable<DomainType> EntitiesToDomains(IEnumerable<EntityType> entities)
        {
            var models = new List<DomainType>();
            foreach (var entity in entities)
            {
                models.Add(this.EntityToDomain(entity));
            }

            return models;
        }

        protected virtual void SetEntityState(DomainType model, EntityState entityState)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model cannot be null!");
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

        protected virtual void InitializeEntityToDomainMapper()
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());
        }

        protected virtual DomainType MapEntityToDomain(EntityType entity)
        {
            var domain = Mapper.Map<EntityType, DomainType>(entity);
            return domain;
        }

        private IEnumerable<DomainType> MapEntitiesToDomains(IEnumerable<EntityType> entities)
        {
            this.InitializeEntityToDomainMapper();

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
