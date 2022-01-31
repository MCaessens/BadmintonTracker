using System.Linq;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class ShuttleCockRepository : BaseRepository<ShuttleCock>, IShuttleCockRepository
    {
        public ShuttleCockRepository(BadmintonDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<ShuttleCock> GetAll()
        {
            return DbContext.ShuttleCocks
                .Include(sc => sc.User)
                .Include(sc => sc.RelatedGames);
        }
    }
}