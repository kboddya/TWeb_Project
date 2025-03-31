using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        [StringLength(30)]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "email")]
        [StringLength(30)]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "password")]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
        
        [StringLength(30)]
        public string LastIp { get; set; }
        
        public URole Role { get; set; }
        
        public DateTime LastLoginTime { get; set; }
        
        public DateTime RegisterTime { get; set; }
    }
}
