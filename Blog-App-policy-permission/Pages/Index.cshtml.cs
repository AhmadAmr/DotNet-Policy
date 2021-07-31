using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_App_policy_permission.Pages
{
    [AllowAnonymous]
    public class HelloModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
