using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Blog_App_policy_permission.Pages.Blog
{
    [Authorize("Permissions.Posts.Edit")]
    public class EditModel : PageModel
    {
        private readonly Blog_App_policy_permission.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(Blog_App_policy_permission.Data.ApplicationDbContext context , UserManager<IdentityUser> userManager , IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Models.Blog Blog { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blogs.Include(x => x.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (Blog == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Blog, "Permission.Posts.Edit");
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           

            var editHistorys = new EditHistory
            {
                UpdatedOn = DateTime.Now,
                user = await _userManager.GetUserAsync(User)
            };

            Blog.EditHistories = new List<EditHistory> { editHistorys };
            _context.editHistories.Add(editHistorys);
            _context.Attach(Blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(Blog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
