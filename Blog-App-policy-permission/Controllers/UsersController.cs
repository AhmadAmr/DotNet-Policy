using Blog_App_policy_permission.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager ,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Select(x => new UserViewModel { id = x.Id , UserName = x.UserName , Email = x.Email ,Roles = _userManager.GetRolesAsync(x).Result})
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var ViewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(x => new CheckBoxViewModel {

                    Value = x.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, x.Name).Result

                }).ToList()
            
            };


            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoles(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var oldRoles = await _userManager.GetRolesAsync(user);

            var currentRoles = model.Roles.Where(x => x.IsSelected).Select(x => x.Value);

            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRolesAsync(user, currentRoles);


            return RedirectToAction(nameof(Index));
        }

    }
}
