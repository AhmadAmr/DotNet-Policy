using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog_App_policy_permission.Models
{
    public class EditHistory
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedOn { get; set; }
        public virtual IdentityUser user { get; set; }
        //navigation
        public int BlogId { get; set; }
        public virtual Blog blog { get; set; }
    }
}