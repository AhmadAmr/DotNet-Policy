using Blog_App_policy_permission.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Authorization
{
     //OperationAuthorizationRequirement
    // OperationAuthorizationRequirement.Name
    public class BlogOwnerAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public BlogOwnerAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null )
            {
                return Task.CompletedTask;
            }

            

            if (context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission ))
            {
               // return Task.CompletedTask;
                context.Succeed(requirement);
            }

            //if (resource.Author.Id == _userManager.GetUserId(context.User))
            //{
            //    context.Succeed(requirement);
            //}

            return Task.CompletedTask;
        }
    }
}
