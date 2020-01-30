using System;
using System.Threading.Tasks;
using Foodoku.Authorization;
using Foodoku.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Foodoku.Authorization
{
    public class AdminUserAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, BindingAgent>
    {
        UserManager<IdentityUser> _userManager;

        public AdminUserAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            BindingAgent resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
