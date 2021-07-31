using Blog_App_policy_permission.Authorization;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Authorization
{
     //OperationAuthorizationRequirement
    // OperationAuthorizationRequirement.Name
    public class BlogAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement )
        {
            if (context.User == null )
            {
                return Task.CompletedTask;
            }

           

            if (context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission ))
            {
               
                context.Succeed(requirement);
            }

           
            return Task.CompletedTask;
        }

    }
}
