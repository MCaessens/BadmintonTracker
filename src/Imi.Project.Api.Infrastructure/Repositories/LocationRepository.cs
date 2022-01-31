using System.Linq;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(BadmintonDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Location> GetAll()
        {
            return DbContext.Locations
                .Include(l => l.User)
                .Include(l => l.RelatedGames);
        }
    }
}