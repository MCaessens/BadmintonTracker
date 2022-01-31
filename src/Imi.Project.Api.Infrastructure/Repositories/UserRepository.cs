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
    public class UserRepository : IUserRepository
    {
        private readonly BadmintonDbContext _dbContext;
        public UserRepository(BadmintonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(User entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entityToDelete = await GetAll().FirstOrDefaultAsync(u => u.Id == id);
            if (entityToDelete != null)
            {
                _dbContext.Remove(entityToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public int GetItemCount()
        {
            return GetAll().Count();
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Set<User>()
                             .Include(u => u.Games)
                             .Include(u => u.ShuttleCocks)
                             .Include(u => u.Locations)
                             .Include(u => u.Rackets);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> ListAllAsync(PageParameters pageParameters)
        {
            return await GetAll().ToListAsync();
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var entityToUpdate = GetByIdAsync(entity.Id);
            if (entityToUpdate != null)
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;

        }
    }
}
