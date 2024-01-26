using Application.Abstractions.Services;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private RoleEnum[] Roles { get; set; }
        private bool IsMatchAll { get; set; }
        public JwtAuthorizeAttribute(params RoleEnum[] roles)
        {
            Roles= roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            UnauthorizedObjectResult? unauthorizedResult = new UnauthorizedObjectResult(ApiResult.Unauthorized());

            // service call
            var _currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

            if (_currentUserService == null)
            {
                context.Result = unauthorizedResult;
                return;
            }

            // check user roles
            if (Roles.Count() > 0)
            {
                bool hasRight = Roles.Any(x => (int)x == _currentUserService.RoleId);
                if (!hasRight)
                {
                    context.Result = unauthorizedResult;
                    return;
                }                
            }
        }
    }
}
