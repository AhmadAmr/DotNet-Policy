using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_App_policy_permission.Data;
using Blog_App_policy_permission.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog_App_policy_permission.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _contex;

        public IndexModel(ApplicationDbContext context)
        {
            _contex = context;
        }
        
        public IList<Models.Blog> Blog { get; private set; }
        public async Task OnGetAsync()
        {
            var blog = _contex.Blogs.Where(x => x.Published && x.Status == BlogStatus.Approved);
            Blog = await blog.ToListAsync();
        }
    }
}
