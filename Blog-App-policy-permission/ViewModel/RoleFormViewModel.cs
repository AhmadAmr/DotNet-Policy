using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_App_policy_permission.ViewModel
{
    public class RoleFormViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
