using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<User> GetByIdAsync(Guid id);

        Task<IEnumerable<User>> ListAllAsync(PageParameters pageParameters);

        Task<bool> UpdateAsync(User entity);

        Task<bool> AddAsync(User entity);

        Task<bool> DeleteAsync(Guid id);
        int GetItemCount();
    }
}