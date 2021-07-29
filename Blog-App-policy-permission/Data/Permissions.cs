using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.Data
{
    public static class Permissions
    {
        public static List<string> permissionList(string module)
        {
            return new List<string>
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Approve",
                $"Permissions.{module}.Reject",
            };
        }

        public static List<string> AllPermission()
        {
            var modules = Enum.GetValues(typeof(Modules));
            var permission = new List<string>();

            foreach (var module in modules)
            {
                permission.AddRange(permissionList(module.ToString()));
            }

            return permission;
        }
    }
}
