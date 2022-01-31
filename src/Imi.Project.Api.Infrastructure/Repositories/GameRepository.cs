using System.Linq;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(BadmintonDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Game> GetAll()
        {
            return DbContext.Games
                .Include(g => g.Racket)
                .Include(g => g.User)
                .Include(g => g.ShuttleCock)
                .Include(g => g.Location);
        }
    }
}