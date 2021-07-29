using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.ViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<CheckBoxViewModel> Roles { get; set; }
    }
}
