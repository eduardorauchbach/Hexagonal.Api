
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;
using Hexagonal.Session;

namespace GestaoVarejoTwoS.Api.Controllers.Helper
{
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomControllerBase : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomControllerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _unitOfWork.BeginTransaction();

            //Here the execution will run
            var executedContext = await next.Invoke();

            if (executedContext.Exception is null)
            {
                try
                {
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    executedContext.Exception = ex;
                }
            }

            HandleKnownExceptions(executedContext);
        }

        private static void HandleKnownExceptions(ActionExecutedContext executedContext)
        {
            if (executedContext.Exception is null) return;

            var response = new
            {
                executedContext.Exception.Message,
                executedContext.Exception.StackTrace
            };

            executedContext.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            executedContext.ExceptionHandled = true;
        }
    }
}
