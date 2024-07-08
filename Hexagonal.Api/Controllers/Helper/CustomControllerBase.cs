using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Hexagonal.Session;
using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.Api.Controllers.Helper
{
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomControllerBase : Controller
    {
        protected readonly ITokenService TokenService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomControllerBase(IUnitOfWork unitOfWork, ITokenService tokenService = null)
        {
            _unitOfWork = unitOfWork;
            TokenService = tokenService;
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

            executedContext.HandleKnownExceptions();
        }

        protected bool HasPermission(out ActionResult result)
        {
            result = null;

            if (!TokenService.IsAdmin(HttpContext))
            {
                result = (ActionResult)Result.Failure(Messages.GenericNotAllowedForUser, HttpStatusCode.MethodNotAllowed);
                return false;
            }
            return true;
        }

        protected bool HasPermission(ProfileAreaType area, out ActionResult result, bool add = false, bool update = false, bool delete = false)
        {
            result = null;

            if (!TokenService.HasPermission(HttpContext, area, add, update, delete))
            {
                result = (ActionResult)Result.Failure(Messages.GenericNotAllowedForUser, HttpStatusCode.MethodNotAllowed);
                return false;
            }
            return true;
        }
    }
}
