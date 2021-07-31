using Blog_App_policy_permission.Authorization;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Authorization
{
    public class BlogAdministratorsAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement )
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            
            if (context.User.IsInRole(Roles.SuperAdmin.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
