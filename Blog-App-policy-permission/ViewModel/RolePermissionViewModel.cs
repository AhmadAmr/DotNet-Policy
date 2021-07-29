using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.ViewModel
{
    public class RolePermissionViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public List<CheckBoxViewModel> Permissions { get; set; }
    }
}
