using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hexagonal.Api.Controllers.Helper
{
    public static class ExceptionsHelper
    {
        public static void HandleKnownExceptions(this ActionExecutedContext executedContext)
        {
            if (executedContext.Exception is null) return;

            if (executedContext.Exception.Message.Contains("23503:"))
            {
                var response = new
                {
                    Message = "Não é possível excluir itens em uso"
                };

                executedContext.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
            }
            else
            {
                var response = new
                {
                    executedContext.Exception.Message,
                    executedContext.Exception.StackTrace
                };

                executedContext.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            executedContext.ExceptionHandled = true;
        }
    }
}
