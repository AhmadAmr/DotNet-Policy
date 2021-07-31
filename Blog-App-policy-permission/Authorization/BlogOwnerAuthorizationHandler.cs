using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Authorization
{
    public class BlogOwnerAuthorizationHandler : AuthorizationHandler<PermissionRequirement, Blog>
    {
        private UserManager<IdentityUser> _userManager;

        public BlogOwnerAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement, Blog resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (resource.Author.Id == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
