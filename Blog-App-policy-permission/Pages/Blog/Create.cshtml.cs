using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Blog_App_policy_permission.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Blog_App_policy_permission.Pages
{
    [Authorize("Permissions.Posts.Create")]
    public class CreateModel : PageModel
    {
        private readonly Blog_App_policy_permission.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BlogService _service;

        public CreateModel(Blog_App_policy_permission.Data.ApplicationDbContext context, BlogService service , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Blog Blog { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Blog.Author = await _userManager.GetUserAsync(User);
            Blog.CreatedOn = DateTime.Now;
            Blog.Image = _service.UploadedFile(Image);

            _context.Blogs.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
