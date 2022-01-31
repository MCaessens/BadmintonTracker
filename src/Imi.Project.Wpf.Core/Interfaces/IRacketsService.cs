using Imi.Project.Wpf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Wpf.Core.Interfaces
{
    public interface IRacketsService
    {
        Task<BaseApiModel<RacketModel>> GetAllRacketsAsync();
    }
}
