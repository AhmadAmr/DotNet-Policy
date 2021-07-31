using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog_App_policy_permission.Pages.Blog
{
    [Authorize("Permissions.Posts.Delete")]
    public class DeleteModel : PageModel
    {
        private readonly Blog_App_policy_permission.Data.ApplicationDbContext _context;

        public DeleteModel(Blog_App_policy_permission.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blogs.FindAsync(id);

            if (Blog != null)
            {
                _context.Blogs.Remove(Blog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
