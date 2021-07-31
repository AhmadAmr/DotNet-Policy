using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;

namespace Blog_App_policy_permission.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly Blog_App_policy_permission.Data.ApplicationDbContext _context;

        public DetailsModel(Blog_App_policy_permission.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
