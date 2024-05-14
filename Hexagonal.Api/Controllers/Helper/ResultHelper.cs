using Hexagonal.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal.Api.Controllers.Helper
{
    internal static class ResultHelper
    {
        public static ActionResult ToActionResult<T>(this Result<T> result) where T : class
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }
            else
            {
                return new ObjectResult(new { result.Message })
                {
                    StatusCode = (int)result.StatusCode
                };
            }
        }

        public static ActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }
            else
            {
                return new ObjectResult(new { result.Message })
                {
                    StatusCode = (int)result.StatusCode
                };
            }
        }
    }
}
