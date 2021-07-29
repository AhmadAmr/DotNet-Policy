using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public  async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.ToListAsync();


            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleFormViewModel role)
        {

            if(ModelState.IsValid)
            {
                if(await _roleManager.RoleExistsAsync(role.Name))
                {
                    ModelState.AddModelError("Name", "Role is exists!");
                    return View("Index", await _roleManager.Roles.ToListAsync());
                }

                await _roleManager.CreateAsync(new IdentityRole(role.Name.Trim()));
                var admin =  await _userManager.GetUserAsync(User);
                if(admin !=null)
                     await _userManager.AddToRoleAsync(admin, role.Name);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _roleManager.Roles.ToListAsync());
        }

        public async Task<IActionResult> Managepermission(string roleid)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
                return NotFound();

            var roleClaims =  _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();

            var allpermession = Permissions.AllPermission().Select(p => new CheckBoxViewModel {
                
                Value = p ,
                IsSelected = roleClaims.Any(c => c==p)

            }).ToList() ;

            var viewModel = new RolePermissionViewModel 
            { 
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = allpermession
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(RolePermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return NotFound();

            foreach (var claim in await _roleManager.GetClaimsAsync(role))
                await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.Permissions.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.Value));

            return RedirectToAction(nameof(Index));
        }
    }
}
