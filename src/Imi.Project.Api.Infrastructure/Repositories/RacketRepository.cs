using System.Linq;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class RacketRepository : BaseRepository<Racket>, IRacketRepository
    {
        public RacketRepository(BadmintonDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Racket> GetAll()
        {
            return DbContext.Rackets
                .Include(r => r.User)
                .Include(r => r.RelatedGames);
        }
    }
}