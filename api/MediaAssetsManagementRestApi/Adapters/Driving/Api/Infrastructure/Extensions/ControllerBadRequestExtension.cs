using Api.Controllers.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Extensions
{
    public static class ControllerBadRequestExtension
    {
        public static IActionResult BadRequest(this ControllerBase controller, ApiErrors apiError)
        {
            return new BadRequestObjectResult(new
            {
                ErrorCode = apiError.Id,
                ErrorMessage = apiError.Value
            });
        }
    }
}
