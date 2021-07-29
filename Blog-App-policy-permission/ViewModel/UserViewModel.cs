using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.ViewModel
{
    public class UserViewModel
    {
        public string id { get; set; }

        public string UserName { get; set; }
        public string  Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
