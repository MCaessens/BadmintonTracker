using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Api.Core.Helpers
{
    public static class ServiceHelper
    {
        // Ok
        public static OkResult Ok() => new OkResult();
        public static OkObjectResult Ok(object obj) => new OkObjectResult(obj); 

        // BadRequest
        public static BadRequestResult BadRequest() => new BadRequestResult();
        public static BadRequestObjectResult BadRequest(object obj) => new BadRequestObjectResult(obj);

        // NotFound
        public static NotFoundResult NotFound() => new NotFoundResult();
        public static NotFoundObjectResult NotFound(object obj) => new NotFoundObjectResult(obj);

        // Conflict
        public static ConflictResult Conflict() => new ConflictResult();
        public static ConflictObjectResult Conflict(object obj) => new ConflictObjectResult(obj);

        // Created
        public static CreatedResult Created(string location, object value) => new CreatedResult(location, value);
    }
 }
