using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog_App_policy_permission.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly Blog_App_policy_permission.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(Blog_App_policy_permission.Data.ApplicationDbContext context , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.Blog Blog { get; set; }

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
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, BlogStatus status)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);

            if (blog == null)
                return NotFound();

            
            blog.Status = status;
            if (status == BlogStatus.Approved)
                blog.Approver = await _userManager.GetUserAsync(User);
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
