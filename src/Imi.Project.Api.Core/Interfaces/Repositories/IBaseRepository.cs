using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        Task<bool> DeleteAsync(Guid id);

        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> ListAllAsync(PageParameters pageParameters);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteMultipleByUserIdAsync(Guid id);
        int GetItemCount();
    }
}