using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Data
{
    public static class InitialUser
    {
        public static async Task seedbasicuser(UserManager<IdentityUser> userManager)
        {
            var basicuser = new IdentityUser
            {
                UserName = "a@a.com",
                Email = "a@a.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(basicuser.Email);

            if(user == null)
            {
               await userManager.CreateAsync(basicuser, "P@ssw0rd123");
               await userManager.AddToRoleAsync(basicuser, Roles.Basic.ToString());
            }

        }
        public static async Task seedsuperadmin(UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManger)
        {
            var superadmin = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(superadmin.Email);

            if (user == null)
            {
                await userManager.CreateAsync(superadmin, "P@ssw0rd123");
                var roles = new List<string>();
                foreach (var role in Enum.GetValues(typeof(Roles)))
                    roles.Add(role.ToString());
                await userManager.AddToRolesAsync(superadmin, roles);
            }
            await roleManger.SeedClaimsForSuperUser();
        }

        private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddPermissionClaims(adminRole, "Products");
        }

        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.permissionList(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
