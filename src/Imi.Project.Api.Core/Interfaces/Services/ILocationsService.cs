using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos.Locations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface ILocationsService
    {
        Task<IActionResult> ListAllAsync(PageParameters pageParameters);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> AddAsync(LocationRequestDto locationRequestDto);
        Task<IActionResult> DeleteAsync(Guid id);
        Task<IActionResult> UpdateAsync(LocationRequestDto locationRequestDto);
        Task<IActionResult> GetLocationsByUserId(Guid id, PageParameters pageParameters);
    }
}
