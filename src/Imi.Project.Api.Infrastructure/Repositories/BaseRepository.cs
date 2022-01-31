using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BadmintonDbContext DbContext;

        protected BaseRepository(BadmintonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entityToDelete = await DbContext.FindAsync(typeof(T), id);
            if (entityToDelete == null) return false;

            DbContext.Remove(entityToDelete);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMultipleByUserIdAsync(Guid id)
        {
            var entities = DbContext.Set<T>().Where(e => e.Id == id);

            if (!entities.Any()) return false;

            DbContext.RemoveRange(entities);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public virtual IQueryable<T> GetAll()
        {
            var entities = DbContext.Set<T>();
            return entities;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await GetAll().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync(PageParameters pageParameters)
        {
            var entityList = await GetAll()
                .OrderBy(e => e.Id)
                .Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();
            return entityList;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var entityToUpdate = await GetByIdAsync(entity.Id);
            if (entityToUpdate == null) return null;

            DbContext.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public int GetItemCount()
        {
            return GetAll().Count();
        }
    }
}